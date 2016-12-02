using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Comments
{
    /// <summary>
    /// 评论。
    /// </summary>
    [Table("Comments")]
    public class Comment
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        [Identity]
        public int Id { get; set; }
        
        /// <summary>
        /// 对象ID。
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        [Size(256)]
        public string Title { get; set; }

        /// <summary>
        /// 对象地址。
        /// </summary>
        [Size(256)]
        public string TargetUrl { get; set; }

        /// <summary>
        /// 扩展名称。
        /// </summary>
        [Size(64)]
        public string ExtensionName { get; set; }
    }
}