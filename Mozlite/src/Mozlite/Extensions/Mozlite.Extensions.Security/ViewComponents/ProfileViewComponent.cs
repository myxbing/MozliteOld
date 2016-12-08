using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Security.ViewComponents
{
    /// <summary>
    /// 档案试图组件。
    /// </summary>
    public class ProfileViewComponent : ViewComponent
    {
        private readonly IUserManager _userManager;
        private readonly User _currentUser;

        /// <summary>
        /// 初始化类<see cref="ProfileViewComponent"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="currentUser">当前登陆用户。</param>
        public ProfileViewComponent(IUserManager userManager, User currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
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
                    profile = await _userManager.GetProfileAsync(_currentUser);
                else
                    profile = await _userManager.GetProfileAsync(name);
                HttpContext.Items[typeof(ProfileViewComponent)] = profile;
            }
            return View(profile);
        }
    }
}