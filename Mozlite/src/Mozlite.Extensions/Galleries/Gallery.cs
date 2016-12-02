using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Galleries
{
    /// <summary>
    /// 图库类型。
    /// </summary>
    [Table("Galleries")]
    public class Gallery
    {
        /// <summary>
        /// ID。
        /// </summary>
        [Identity]
        public int Id { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 访问量。
        /// </summary>
        public int Views { get; set; }
    }
}