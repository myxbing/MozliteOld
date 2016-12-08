using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mozlite.Extensions.Identity;
using Mozlite.Extensions.Security.DisallowNames;
using Mozlite.Extensions.Security.ViewModels;
using System.Linq;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 后台管理。
    /// </summary>
    public class AdminController : AdminControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly User _currentUser;
        private readonly INameManager _nameManager;
        private readonly ILogger _logger;

        public AdminController(IUserManager userManager, User currentUser, INameManager nameManager, ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _nameManager = nameManager;
            _logger = logger;
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

        /// <summary>
        /// 添加用户。
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUser model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
                return Warning(Resources.UserNameNotNull);

            if (string.IsNullOrWhiteSpace(model.Password))
                return Warning(Resources.PasswordNotNull);

            if (model.Password != model.ConfirmPassword)
                return Warning(Resources.PasswordAndConfirmPasswordNotEqual);

            model.UserName = model.UserName.Trim();
            model.Password = model.Password.Trim();
            if (model.Password.Length < 6)
                return Warning(Resources.PasswordInvalid);

            if (model.IgnoreDisallowNames && await _nameManager.IsDisallowedAsync(model.UserName))
                return Warning(Resources.UserNameDisallowed);

            if (await _userManager.CheckUserNameAsync(model.UserName))
                return Warning(Resources.UserNameDuplicate);

            var user = new User();
            user.UserName = model.UserName;
            user.NickName = model.UserName;
            user.Email = model.Email;
            user.EmailConfirmed = model.IgnoreEmailConfirm;
            var result = await _userManager.CreateAsync(user, SecurityHelper.CreatePassword(model.UserName, model.Password), IdentitySettings.Register);
            if (result.Succeeded)
            {
                if (model.IgnoreEmailConfirm)
                    return Success("你已经成功添加了用户！");
                return Success("你已经成功添加了用户，可以让用户登陆邮件激活账户！");
            }
            _logger.LogWarning(SecuritySettings.EventId, "注册账户[{0}]失败.", user.UserName);
            var error = result.Errors.FirstOrDefault();
            if (error != null)
                return Error(Resources.ResourceManager.GetString(error.Code));
            return Error("添加用户失败！");
        }

        public IActionResult Lockout(string id)
        {
            return View(new LockoutUser { UserIds = id });
        }

        [HttpPost]
        public IActionResult Lockout(LockoutUser model)
        {
            if (string.IsNullOrWhiteSpace(model.UserIds))
                return Error("用户不存在！");
            if (_userManager.LockoutUsers(model.UserIds.SplitToInt32(), model.LockEnd))
                return Success("你已经成功锁定了账户！");
            return Error("锁定选择账户失败！");
        }

        [HttpPost]
        public IActionResult Unlock(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                return Error("用户不存在！");
            if (_userManager.LockoutUsers(ids.SplitToInt32(), null))
                return Success("你已经成功解锁了账户！");
            return Error("解锁选择账户失败！");
        }

        [HttpPost]
        public IActionResult Delete(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                return Error("用户不存在！");
            if (_userManager.DeleteUsers(ids.SplitToInt32()))
                return Success("你已经成功删除了账户！");
            return Error("删除选择账户失败！");
        }
    }
}