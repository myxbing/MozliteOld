using Microsoft.AspNetCore.Identity;
using Mozlite.Data;
using Mozlite.Extensions.Identity;

namespace Mozlite.Extensions.Security.Data
{
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
        public RoleManager(IRepository<Role> repository, IRoleStore<Role> store) : base(repository, store)
        {
        }
    }
}