using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签关联表
    /// </summary>
    public abstract class TagIndexerBase
    {
        /// <summary>
        /// 标签Id。
        /// </summary>
        [Key]
        public int TagId { get; set; }

        /// <summary>
        /// 模型Id。
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}