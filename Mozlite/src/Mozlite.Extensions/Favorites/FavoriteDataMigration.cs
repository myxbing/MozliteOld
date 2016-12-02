using Mozlite.Data.Migrations;
using Mozlite.Extensions.Security;

namespace Mozlite.Extensions.Favorites
{
    /// <summary>
    /// 收藏数据库迁移类。
    /// </summary>
    public class FavoriteDataMigration : DataMigration<Favorite>
    {
        /// <summary>
        /// 当模型建立时候构建的表格实例。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        protected override void Create(MigrationBuilder<Favorite> builder)
        {
            builder.CreateTable(t => t
                .Column(x => x.UserId)
                .Column(x => x.TargetId)
                .Column(x => x.Title)
                .Column(x => x.ExtensionName)
                .Column(x => x.CreatedDate)
                .Column(x => x.Description)
                .Column(x => x.Logo)
                .ForeignKey<User>(x => x.UserId, x => x.UserId, onDelete: ReferentialAction.Cascade)
            );
        }
    }
}