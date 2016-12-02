namespace Mozlite.Extensions.Comments
{
    /// <summary>
    /// 可评论接口。
    /// </summary>
    public interface ICommentable
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 评论Id。
        /// </summary>
        int CommentId { get; set; }

        /// <summary>
        /// 评论数量。
        /// </summary>
        int Comments { get; set; }

        /// <summary>
        /// 是否允许评论。
        /// </summary>
        bool EnabledComment { get; set; }
    }
}