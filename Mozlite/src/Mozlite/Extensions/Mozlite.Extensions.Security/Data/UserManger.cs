using System;
using Mozlite.Data;
using Mozlite.FileProviders;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mozlite.Extensions.Identity;
using Microsoft.AspNetCore.Identity;

namespace Mozlite.Extensions.Security.Data
{
    /// <summary>
    /// 用户管理类。
    /// </summary>
    public class UserManger : IdentityUserManager<User>, IUserManager
    {
        private readonly IRepository<UserProfile> _db;
        private readonly IMediaFileProvider _fileProvider;

        /// <summary>
        /// 初始化类<see cref="UserManger"/>。
        /// </summary>
        /// <param name="userManager">用户管理实例。</param>
        /// <param name="db">用户档案数据库操作接口。</param>
        /// <param name="repository">数据库操作接口。</param>
        /// <param name="httpContextAccessor">HTTP上下文访问接口。</param>
        /// <param name="fileProvider">媒体文件提供者。</param>
        public UserManger(UserManager<User> userManager, IRepository<UserProfile> db, IRepository<User> repository, IHttpContextAccessor httpContextAccessor, IMediaFileProvider fileProvider) : base(userManager, repository, httpContextAccessor)
        {
            _db = db;
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// 上传头像。
        /// </summary>
        /// <param name="file">上传的文件实例对象。</param>
        /// <param name="id">用户Id。</param>
        /// <returns>返回上传结果。</returns>
        public async Task<string> UploadAvatarAsync(IFormFile file, int id)
        {
            var user = await FindByIdAsync(id);
            if (user == null)
                return null;
            var avatar = user.Avatar;
            if (avatar.IsLocalMediaUrl())
                avatar = await _fileProvider.UploadAsync(file, user.Avatar, SecuritySettings.ExtensionName, id);
            else
                avatar = await _fileProvider.UploadAsync(file, SecuritySettings.ExtensionName, id);
            if (await Repository.UpdateAsync(m => m.UserId == id, new { avatar, UpdatedDate = DateTimeOffset.Now }))
                return avatar;
            return null;
        }

        /// <summary>
        /// 判断用户名是否已经存在。
        /// </summary>
        /// <param name="userName">用户名称。</param>
        /// <returns>返回判断结果。</returns>
        public Task<bool> CheckUserNameAsync(string userName)
        {
            return Repository.AnyAsync(x => x.NormalizedUserName == userName || x.NickName == userName);
        }

        /// <summary>
        /// 获取当前用户档案。
        /// </summary>
        /// <param name="user">用户实例。</param>
        /// <returns>返回用户档案实例。</returns>
        public UserProfile GetProfile(User user)
        {
            var profile = _db.Find(x => x.Id == user.UserId);
            if (profile == null)
            {
                profile = new UserProfile { Id = user.UserId };
                if (!_db.Create(profile))
                    return null;
            }
            profile.SetUser(user);
            return profile;
        }

        /// <summary>
        /// 获取当前用户档案。
        /// </summary>
        /// <param name="user">用户实例。</param>
        /// <returns>返回用户档案实例。</returns>
        public async Task<UserProfile> GetProfileAsync(User user)
        {
            var profile = await _db.FindAsync(x => x.Id == user.UserId);
            if (profile == null)
            {
                profile = new UserProfile { Id = user.UserId };
                if (!await _db.CreateAsync(profile))
                    return null;
            }
            profile.SetUser(user);
            return profile;
        }

        /// <summary>
        /// 获取当前用户档案。
        /// </summary>
        /// <param name="userName">用户名称。</param>
        /// <returns>返回用户档案实例。</returns>
        public async Task<UserProfile> GetProfileAsync(string userName)
        {
            var user = await FindByNameAsync(userName);
            if (user == null)
                return null;
            var profile = await _db.FindAsync(x => x.Id == user.UserId);
            if (profile == null)
            {
                profile = new UserProfile { Id = user.UserId };
                if (!await _db.CreateAsync(profile))
                    return null;
            }
            profile.SetUser(user);
            return profile;
        }

        /// <summary>
        /// 修改个人简介。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="intro">用户介绍。</param>
        /// <returns>返回更新结果。</returns>
        public async Task<DataResult> ChangeIntroAsync(int userId, string intro)
        {
            return DataResult.FromResult(await _db.UpdateAsync(x => x.Id == userId, new { intro }), DataAction.Updated);
        }
    }
}