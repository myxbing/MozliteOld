using Mozlite.Data.Migrations;
using Mozlite.Extensions.Categories;

namespace Mozlite.Extensions.Archives.Data
{
    /// <summary>
    /// 分类数据库迁移实例。
    /// </summary>
    public class CategoryDataMigration : ComplexCategoryDataMigration<Category>
    {
        /// <summary>
        /// 创建操作。
        /// </summary>
        /// <param name="builder">迁移构建实例对象。</param>
        protected override void Create(MigrationBuilder<Category> builder)
        {
            base.Create(builder);
            builder.AddColumn(x => x.DisplayName);
        }
    }
}