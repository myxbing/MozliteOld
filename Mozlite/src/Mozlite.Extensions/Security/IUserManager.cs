using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mozlite.Core;
using Mozlite.Extensions.Identity;
using Mozlite.Data;
using Mozlite.FileProviders;

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
    }

    /// <summary>
    /// 用户管理类。
    /// </summary>
    public class UserManger : IdentityUserManager<User>, IUserManager
    {
        private readonly IMediaFileProvider _fileProvider;
        /// <summary>
        /// 初始化类<see cref="UserManger"/>。
        /// </summary>
        /// <param name="userManager">用户管理实例。</param>
        /// <param name="repository">数据库操作接口。</param>
        /// <param name="httpContextAccessor">HTTP上下文访问接口。</param>
        /// <param name="fileProvider">媒体文件提供者。</param>
        public UserManger(UserManager<User> userManager, IRepository<User> repository, IHttpContextAccessor httpContextAccessor, IMediaFileProvider fileProvider) : base(userManager, repository, httpContextAccessor)
        {
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// 上传头像。
        /// </summary>
        /// <param name="file">上传的文件实例对象。</param>
        /// <param name="id">用户Id。</param>
        /// <returns>返回上传结果。</returns>
        public async Task<bool> UploadAvatarAsync(IFormFile file, int id)
        {
            var user = await FindByIdAsync(id);
            if (user == null)
                return false;
            var avatar = user.Avatar;
            if (avatar.IsLocalMediaUrl())
                avatar = await _fileProvider.UploadAsync(file, user.Avatar, SecuritySettings.MediaType, id);
            else
                avatar = await _fileProvider.UploadAsync(file, SecuritySettings.MediaType, id);
            if (avatar == null)
                return false;
            return await Repository.UpdateAsync(m => m.UserId == id, new { avatar, UpdatedDate = DateTime.Now });
        }
    }
}