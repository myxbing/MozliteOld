using Mozlite.Data.Metadata;
using Mozlite.Extensions.Follows;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签。
    /// </summary>
    [Table("Tags")]
    public class Tag : IFollowable
    {
        /// <summary>
        /// 标签。
        /// </summary>
        [Identity]
        public int Id { get; set; }

        /// <summary>
        /// 收藏数量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Follows { get; set; }

        /// <summary>
        /// 分类Id。
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 标签名称。
        /// </summary>
        [Key]
        [Size(64)]
        public string Name { get; set; }

        /// <summary>
        /// 图标地址。
        /// </summary>
        [Size(256)]
        public string IconUrl { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        [Size(200)]
        public string Description { get; set; }

        /// <summary>
        /// 标签百科，详细信息。
        /// </summary>
        [Ignore(Ignore.List | Ignore.Update)]
        public string Body { get; set; }
    }
}