using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Security.ViewModels;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 后台管理。
    /// </summary>
    public class AdminController : AdminControllerBase
    {
        private readonly IUserManager _userManager;
        public AdminController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(UserQuery query)
        {
            return View(_userManager.Load(query));
        }

        public IActionResult SetPassword(int id)
        {
            return View(new SetPasswordViewModel { UserId = id });
        }

        [HttpPost]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
                return Warning(Resources.PasswordNotNull);

            if (model.Password != model.ConfirmPassword)
                return Warning(Resources.PasswordAndConfirmPasswordNotEqual);

            model.Password = model.Password.Trim();
            if (model.Password.Length < 6)
                return Warning(Resources.PasswordInvalid);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return Error("用户不存在！");
            var result =
                await
                    _userManager.ResetPasswordAsync(user, SecurityHelper.CreatePassword(user.UserName, model.Password));
            if (result.Succeeded)
            {
                return Success($"你已经成功设置了“{user.NickName}”的密码！");
            }
            return Error("设置密码失败！");
        }
    }
}