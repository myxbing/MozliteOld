using Microsoft.AspNetCore.Identity;
using Mozlite.Core;
using Mozlite.Extensions.Identity;
using Mozlite.Data;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 用户组管理接口。
    /// </summary>
    public interface IRoleManager : IIdentityRoleManager<Role>, IScopedService
    {

    }

    /// <summary>
    /// 用户组管理类。
    /// </summary>
    public class RoleManager : IdentityRoleManager<Role>, IRoleManager
    {
        /// <summary>
        /// 初始化类<see cref="RoleManager"/>。
        /// </summary>
        /// <param name="repository">用户组数据库操作接口。</param>
        /// <param name="store">用户组存储接口实例。</param>
        public RoleManager(IRepository<Role> repository, IRoleClaimStore<Role> store) : base(repository, store)
        {
        }
    }
}