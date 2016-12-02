using System;
using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Comments
{
    /// <summary>
    /// 评论内容。
    /// </summary>
    [Table("Comments_Posts")]
    public class Post
    {
        /// <summary>
        /// 评论Id。
        /// </summary>
        [Identity]
        public int Id { get; set; }

        /// <summary>
        /// 评论Id。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int CommentId { get; set; }

        /// <summary>
        /// 用户Id。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int UserId { get; set; }

        /// <summary>
        /// 用户名称。
        /// </summary>
        [Size(64)]
        [Ignore(Ignore.Update)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像地址。
        /// </summary>
        [Size(256)]
        public string Avatar { get; set; }

        /// <summary>
        /// 父级评论。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int ParentId { get; set; }

        /// <summary>
        /// 回复数量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Replies { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 投票数。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Votes { get; set; }

        /// <summary>
        /// 举报。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Inform { get; set; }

        /// <summary>
        /// 评论时间。
        /// </summary>
        [Ignore(Ignore.Update)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 状态。
        /// </summary>
        public ObjectStatus Status { get; set; }

        /// <summary>
        /// IP地址。
        /// </summary>
        [Size(20)]
        [Ignore(Ignore.Update)]
        public string IP { get; set; }
    }
}