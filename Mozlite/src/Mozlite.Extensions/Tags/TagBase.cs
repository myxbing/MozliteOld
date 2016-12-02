using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签。
    /// </summary>
    public abstract class TagBase
    {
        /// <summary>
        /// 标签。
        /// </summary>
        [Identity]
        public int Id { get; set; }

        /// <summary>
        /// 标签名称。
        /// </summary>
        [Size(64)]
        public string Name { get; set; }

        /// <summary>
        /// 标签数量。
        /// </summary>
        public int Count { get; set; }
    }
}