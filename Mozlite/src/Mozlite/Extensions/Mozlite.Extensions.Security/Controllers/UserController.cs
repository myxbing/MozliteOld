using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 用户中心控制器。
    /// </summary>
    public class UserController : UserControllerBase
    {
        private readonly User _currentUser;
        private readonly IUserManager _userManager;

        public UserController(User currentUser, IUserManager userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        /// <summary>
        /// 首页。
        /// </summary>
        [Route("u/{name?}")]
        public async Task<IActionResult> Index(string name = null)
        {
            if (name == null || _currentUser.UserName.Equals(name, StringComparison.OrdinalIgnoreCase))
                return View(_currentUser);
            return View(await _userManager.FindByNameAsync(name));
        }
    }
}