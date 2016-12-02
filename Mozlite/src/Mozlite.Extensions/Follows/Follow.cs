using System;
using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Follows
{
    /// <summary>
    /// 关注。
    /// </summary>
    [Table("Follows")]
    public class Follow
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// 对象Id。
        /// </summary>
        [Key]
        public int TargetId { get; set; }

        /// <summary>
        /// 扩展名称。
        /// </summary>
        [Key]
        [Size(64)]
        public string ExtensionName { get; set; }

        /// <summary>
        /// Logo图片。
        /// </summary>
        [Size(256)]
        public string Logo { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        [Size(256)]
        public string Title { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        [Size(256)]
        public string Description { get; set; }

        /// <summary>
        /// 收藏时间。
        /// </summary>
        [Ignore(Ignore.Update)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}