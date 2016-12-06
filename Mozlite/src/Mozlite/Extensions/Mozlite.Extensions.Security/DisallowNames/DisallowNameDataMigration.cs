using Mozlite.Data.Migrations;

namespace Mozlite.Extensions.Security.DisallowNames
{
    /// <summary>
    /// 非法名称数据迁移类型。
    /// </summary>
    public class DisallowNameDataMigration : DataMigration<DisallowName>
    {
        /// <summary>
        /// 当模型建立时候构建的表格实例。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        protected override void Create(MigrationBuilder<DisallowName> builder)
        {
            builder.CreateTable(table => table
                .Column(x => x.Id)
                .Column(x => x.Name)
            );
        }
    }
}