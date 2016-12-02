using Mozlite.Extensions.Identity;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 数据迁移。
    /// </summary>
    public class SecurityMigration : IdentityDataMigration<User, Role, UserClaim, RoleClaim, UserLogin, UserRole, UserToken>
    {
    }
}