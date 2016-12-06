using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mozlite.Core;
using Mozlite.Extensions.Identity;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 用户管理接口。
    /// </summary>
    public interface IUserManager : IIdentityUserManager<User>, IScopedService
    {
        /// <summary>
        /// 上传头像。
        /// </summary>
        /// <param name="file">上传的文件实例对象。</param>
        /// <param name="id">用户Id。</param>
        /// <returns>返回上传结果。</returns>
        Task<bool> UploadAvatarAsync(IFormFile file, int id);

        /// <summary>
        /// 判断用户名是否已经存在。
        /// </summary>
        /// <param name="userName">用户名称。</param>
        /// <returns>返回判断结果。</returns>
        Task<bool> CheckUserNameAsync(string userName);
    }
}