using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Security.ViewModels;
using System.Linq;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 用户中心控制器。
    /// </summary>
    public class UserController : UserControllerBase
    {
        private readonly User _currentUser;
        private readonly UserProfile _profile;
        private readonly IUserManager _userManager;

        public UserController(UserProfile profile, IUserManager userManager)
        {
            _currentUser = profile.User;
            _profile = profile;
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

        public IActionResult NickName()
        {
            ViewBag.NickName = _currentUser.NickName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NickName(string nickName)
        {
            if (string.IsNullOrWhiteSpace(nickName))
                return Error("昵称不能为空！");
            return Json(await _userManager.ChangeNickNameAsync(_currentUser.UserId, nickName), "昵称");
        }

        public IActionResult Intro()
        {
            ViewBag.Intro = _profile.Intro;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Intro(string intro)
        {
            return Json(await _userManager.ChangeIntroAsync(_currentUser.UserId, intro), "个人介绍");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(IFormFile avatar)
        {
            var url = await _userManager.UploadAvatarAsync(avatar, UserId);
            if (url == null)
                return Error("上传头像失败，请重试！");
            return Success("你已经成功更新了头像！", new { url });
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
                return Error("原始密码不能为空！");

            if (string.IsNullOrWhiteSpace(model.Password))
                return Error("新密码不能为空！");

            model.Password = model.Password.Trim();
            if (model.Password != model.ConfirmPassword?.Trim())
                return Error(Resources.PasswordAndConfirmPasswordNotEqual);

            var result =
                await
                    _userManager.ChangePasswordAsync(_currentUser,
                        SecurityHelper.CreatePassword(_currentUser.UserName, model.Password),
                        SecurityHelper.CreatePassword(_currentUser.UserName, model.NewPassword));
            if (result.Succeeded)
                return Success("你已经成功更新了密码，下次登陆时候需要使用新密码进行登陆！");

            var error = result.Errors.FirstOrDefault();
            if (error != null)
                return Error(Resources.ResourceManager.GetString(error.Code));
            return Error("修改密码失败！");
        }
    }
}