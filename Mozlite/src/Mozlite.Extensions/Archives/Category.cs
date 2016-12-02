using Mozlite.Data.Metadata;
using Mozlite.Extensions.Categories;

namespace Mozlite.Extensions.Archives
{
    /// <summary>
    /// 新闻分类。
    /// </summary>
    [Table("Archives_Categories")]
    public class Category : ComplexCategoryBase<Category>
    {
        /// <summary>
        /// 显示名称。
        /// </summary>
        [Size(64)]
        public string DisplayName { get; set; }
    }
}