using Mozlite.Core;
using Mozlite.Extensions.Identity;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 用户组管理接口。
    /// </summary>
    public interface IRoleManager : IIdentityRoleManager<Role>, IScopedService
    {

    }
}