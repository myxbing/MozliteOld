using Mozlite.Data;

namespace Mozlite.Extensions.Comments
{
    /// <summary>
    /// 评论查询实例。
    /// </summary>
    public class PostQuery : QueryBase<Post>
    {
        /// <summary>
        /// 对象Id。
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 扩展名称。
        /// </summary>
        public string ExtensionName { get; set; }

        /// <summary>
        /// 评论Id。
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 评论内容Id。
        /// </summary>
        public int? PostId { get; set; }

        /// <summary>
        /// 状态。
        /// </summary>
        public ObjectStatus? Status { get; set; }

        /// <summary>
        /// 初始化查询上下文。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected override void Init(IQueryContext<Post> context)
        {
            context.OrderByDescending(x => x.Id);
            if (PostId != null)
                context.Where(x => x.ParentId == PostId);
            if (CommentId == 0)
                context.InnerJoin<Comment>((p, c) => p.CommentId == c.Id)
                    .Where<Comment>(x => x.TargetId == TargetId && x.ExtensionName == ExtensionName);
            else
                context.Where(x => x.CommentId == CommentId);
            if (Status != null)
                context.Where(x => x.Status == Status);
        }
    }
}