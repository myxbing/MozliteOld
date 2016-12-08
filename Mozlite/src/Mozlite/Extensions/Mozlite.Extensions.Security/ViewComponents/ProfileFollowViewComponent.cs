using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Security.ViewComponents
{
    /// <summary>
    /// 关注数量显示。
    /// </summary>
    public class ProfileFollowViewComponent : ViewComponent
    {

        private readonly IUserManager _userManager;
        private readonly UserProfile _profile;

        /// <summary>
        /// 初始化类<see cref="ProfileViewComponent"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="profile">用户档案。</param>
        public ProfileFollowViewComponent(IUserManager userManager, UserProfile profile)
        {
            _userManager = userManager;
            _profile = profile;
        }

        /// <summary>
        /// 获取当前名称的用户实例组件。
        /// </summary>
        /// <returns>返回当前试图组件。</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var profile = HttpContext.Items[typeof(ProfileViewComponent)] as UserProfile;
            if (profile == null)
            {
                var name = RouteData.Values["name"]?.ToString();
                if (name == null)
                    profile = _profile;
                else
                    profile = await _userManager.GetProfileAsync(name);
                HttpContext.Items[typeof(ProfileViewComponent)] = profile;
            }
            return View(profile);
        }
    }
}