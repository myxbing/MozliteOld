using System;
using Mozlite.Core;
using Mozlite.Data;
using Mozlite.Extensions.Security;

namespace Mozlite.Extensions.Comments
{
    /// <summary>
    /// 评论管理接口。
    /// </summary>
    public interface ICommentManager : ISingletonService
    {
        /// <summary>
        /// 添加评论实例，一般程序自动添加。
        /// </summary>
        /// <typeparam name="TTarget">对象类型。</typeparam>
        /// <param name="comment">评论实例对象。</param>
        /// <returns>返回添加结果。</returns>
        bool CreateComment<TTarget>(Comment comment)
            where TTarget : ICommentable;

        /// <summary>
        /// 分页加载评论。
        /// </summary>
        /// <typeparam name="TTarget">对象模型类型。</typeparam>
        /// <param name="target">对象实例。</param>
        /// <param name="title">标题。</param>
        /// <param name="linkUrl">链接地址。</param>
        /// <param name="extensionName">扩展名称。</param>
        /// <param name="action">评论内容查询实例。</param>
        /// <returns>返回评论内容实例。</returns>
        PostQuery LoadPosts<TTarget>(TTarget target, string title, string linkUrl, string extensionName, Action<PostQuery> action)
            where TTarget : ICommentable;

        /// <summary>
        /// 新建评论并且更新目标对象中的评论数量。
        /// </summary>
        /// <typeparam name="TTarget">对象类型。</typeparam>
        /// <param name="post">当前评论内容。</param>
        /// <param name="targetId">目标Id。</param>
        /// <returns>返回添加结果。</returns>
        bool CreatePost<TTarget>(Post post, int targetId)
            where TTarget : ICommentable;
    }

    /// <summary>
    /// 评论管理实现类。
    /// </summary>
    public class CommentManager : ICommentManager
    {
        private readonly IRepository<Post> _posts;
        private readonly IRepository<Comment> _comments;

        public CommentManager(IRepository<Post> posts, IRepository<Comment> comments)
        {
            _posts = posts;
            _comments = comments;
        }

        /// <summary>
        /// 添加评论实例，一般程序自动添加。
        /// </summary>
        /// <typeparam name="TTarget">对象类型。</typeparam>
        /// <param name="comment">评论实例对象。</param>
        /// <returns>返回添加结果。</returns>
        public bool CreateComment<TTarget>(Comment comment)
            where TTarget : ICommentable
        {
            return _comments.BeginTransaction(db =>
            {
                if (!db.Create(comment))
                    return false;
                var target = db.As<TTarget>();
                return target.Update(x => x.Id == comment.TargetId && x.EnabledComment == true, new { CommentId = comment.Id });
            });
        }

        /// <summary>
        /// 分页加载评论。
        /// </summary>
        /// <typeparam name="TTarget">对象模型类型。</typeparam>
        /// <param name="target">对象实例。</param>
        /// <param name="title">标题。</param>
        /// <param name="linkUrl">链接地址。</param>
        /// <param name="extensionName">扩展名称。</param>
        /// <param name="action">评论内容查询实例。</param>
        /// <returns>返回评论内容实例。</returns>
        public PostQuery LoadPosts<TTarget>(TTarget target, string title, string linkUrl, string extensionName, Action<PostQuery> action) where TTarget : ICommentable
        {
            if (target.CommentId == 0)
            {
                var comment = new Comment();
                comment.Title = title;
                comment.ExtensionName = extensionName;
                comment.TargetId = target.Id;
                comment.TargetUrl = linkUrl;
                if (!CreateComment<TTarget>(comment))
                    return null;
                target.CommentId = comment.Id;
            }
            var query = new PostQuery();
            query.CommentId = target.CommentId;
            query.Status = ObjectStatus.Normal;
            query.PostId = 0;//只载入顶级评论
            query.PageSize = 50;
            action(query);
            return _posts.Load(query);
        }

        /// <summary>
        /// 新建评论并且更新目标对象中的评论数量。
        /// </summary>
        /// <typeparam name="TTarget">对象类型。</typeparam>
        /// <param name="post">当前评论内容。</param>
        /// <param name="targetId">目标Id。</param>
        /// <returns>返回添加结果。</returns>
        public bool CreatePost<TTarget>(Post post, int targetId) where TTarget : ICommentable
        {
            return _posts.BeginTransaction(db =>
            {
                if (!db.Create(post))
                    return false;
                var target = db.As<TTarget>();
                return target.IncreaseBy(x => x.Id == targetId, x => x.Comments, 1);
            });
        }
    }
}