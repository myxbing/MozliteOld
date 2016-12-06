using Mozlite.Extensions.Identity;

namespace Mozlite.Extensions.Security.Data
{
    /// <summary>
    /// 数据迁移。
    /// </summary>
    public class SecurityDataMigration : IdentityDataMigration<User, Role, UserClaim, RoleClaim, UserLogin, UserRole, UserToken>
    {
    }
}