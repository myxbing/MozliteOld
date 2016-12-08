using Mozlite.Data.Migrations;
using Mozlite.Extensions.Identity;

namespace Mozlite.Extensions.Security.Data
{
    /// <summary>
    /// 数据迁移。
    /// </summary>
    public class SecurityDataMigration : IdentityDataMigration<User, Role, UserClaim, RoleClaim, UserLogin, UserRole, UserToken>
    {
        public virtual void Up2(MigrationBuilder builder)
        {
            builder.CreateTable<UserProfile>(table => table
                .Column(x => x.Id)
                .Column(x => x.Intro)
                .Column(x => x.Weibo)
                .Column(x => x.Weixin)
                .Column(x => x.CommentId)
                .Column(x => x.Comments)
                .Column(x => x.EnabledComment)
                .Column(x => x.Follows)
                .Column(x => x.Followeds)
                .Column(x => x.QQ)
                .Column(x => x.Score)
                .Column(x => x.ConcurrencyStamp, "timestamp")
                .ForeignKey<User>(x => x.Id, x => x.UserId, onDelete: ReferentialAction.Cascade)
            );
        }

        public virtual void Down2(MigrationBuilder builder)
        {
            builder.DropTable<UserProfile>();
        }

    }
}