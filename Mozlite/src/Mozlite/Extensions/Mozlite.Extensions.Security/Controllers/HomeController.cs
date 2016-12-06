using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Mozlite.Extensions.Identity;
using Mozlite.Extensions.Messages;
using Mozlite.Extensions.Security.DisallowNames;
using Mozlite.Extensions.Security.ViewModels;
using System.Linq;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 用户前台控制器。
    /// </summary>
    public class HomeController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly INameManager _nameManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public HomeController(IUserManager userManager, SignInManager<User> signInManager, ILogger<HomeController> logger, INameManager nameManager, IEmailSender emailSender, ISmsSender smsSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _nameManager = nameManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        /// <summary>
        /// 登陆。
        /// </summary>
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登陆。
        /// </summary>
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser model)
        {
            try
            {
                model.UserName = model.UserName.Trim();
                model.Password = model.Password.Trim();
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, SecurityHelper.CreatePassword(model.UserName, model.Password), model.RememberMe, true);
                if (result.Succeeded)
                {
                    _logger.LogInformation(SecuritySettings.EventId, "用户[{0}]登陆.", model.UserName);
                    if (await _userManager.SignInSuccessAsync(model.UserName))
                    {

                    }
                    return Success();
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToAction(nameof(SendCode),
                //        new { ReturnUrl = Request.GetDisplayUrl(), model.RememberMe });
                //}
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(SecuritySettings.EventId, $"账户[{model.UserName}]被锁定。");
                    return Error("账户被锁定！");
                }
                if (result.IsNotAllowed)
                {
                    return Error("账户未激活，请打开你注册时候的邮箱进行验证！如果没有收到邮件，<span style=\"cursor:pointer;\" onclick=\"$ajax('/user/confirm',{name:'" + model.UserName + "'});\">点击重新发送验证邮件</span>...");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(SecuritySettings.EventId, ex, $"账户[{model.UserName}]登陆失败:{ex.Message}");
            }
            _logger.LogWarning(SecuritySettings.EventId, $"账户[{model.UserName}]登陆失败。");
            return Error("用户名或密码错误！");
        }

        /// <summary>
        /// 注册。
        /// </summary>
        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUser model)
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

            if (await _nameManager.IsDisallowedAsync(model.UserName))
                return Warning(Resources.UserNameDisallowed);

            if (await _userManager.CheckUserNameAsync(model.UserName))
                return Warning(Resources.UserNameDuplicate);

            var user = new User();
            user.UserName = model.UserName;
            user.NickName = model.UserName;
            user.Email = model.Email;
            var result = await _userManager.CreateAsync(user, SecurityHelper.CreatePassword(model.UserName, model.Password), IdentitySettings.Register);
            if (result.Succeeded)
            {
                try
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = ActionUrl("ConfirmEmail", new { userId = user.UserId, code });
                    await
                        _emailSender.SendEmailAsync(user, Resources.Email_ActiveAccount,
                            Resources.Email_ActiveAccount_Body.ReplaceBy(
                                kw =>
                                {
                                    kw.Add("name", user.NickName);
                                    kw.Add("link", callbackUrl);
                                }));
                    _logger.LogInformation(SecuritySettings.EventId, "注册了账户[{0}].", user.UserName);
                    return Success("一封激活邮件已经发送到你的邮箱！该激活链接24小时内有效！");
                }
                catch (Exception exception)
                {
                    _logger.LogError(EmailSettings.EventId, exception, "邮件发送失败：" + exception.Message);
                    return Warning("账户已经成功注册，但是激活邮件发送失败，请联系网站管理员！");
                }
            }
            _logger.LogWarning(SecuritySettings.EventId, "注册账户[{0}]失败.", user.UserName);
            var error = result.Errors.FirstOrDefault();
            if (error != null)
                return Error(Resources.ResourceManager.GetString(error.Code));
            return Error("注册失败！");
        }

        /// <summary>
        /// 退出登录。
        /// </summary>
        /// <returns>退出登录，并转向首页。</returns>
        [HttpPost]
        [Route("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(SecuritySettings.EventId, "退出登陆.");
            return Redirect("/");
        }

        /// <summary>
        /// 发送激活邮件。
        /// </summary>
        [HttpPost]
        [Route("user/confirm")]
        public async Task<IActionResult> ConfirmEmail(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = ActionUrl("ConfirmEmail", new { userId = user.UserId, code });
            await _emailSender.SendEmailAsync(user, Resources.Email_ActiveAccount, Resources.Email_ActiveAccount_Body.ReplaceBy(
                kw =>
                {
                    kw.Add("name", user.NickName);
                    kw.Add("link", callbackUrl);
                }));
            return Success("你已经成功注册账户，一封激活邮件已经发送到你的邮箱！该激活链接24小时内有效！");
        }

        /// <summary>
        /// 激活邮件。
        /// </summary>
        [Route("user/confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        ///// <summary>
        ///// 发送验证码。
        ///// </summary>
        //public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        //{
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ///// <summary>
        ///// 发送验证码。
        ///// </summary>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }

        //    // Generate the token and send it
        //    var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
        //    if (string.IsNullOrWhiteSpace(code))
        //    {
        //        return View("Error");
        //    }

        //    var message = "你的验证码是: " + code;
        //    if (model.SelectedProvider == "Email")
        //    {
        //        await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "[$site;]验证码", message);
        //    }
        //    else if (model.SelectedProvider == "Phone")
        //    {
        //        await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
        //    }

        //    return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        ///// <summary>
        ///// 验证激活码。
        ///// </summary>
        //public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        //{
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ///// <summary>
        ///// 验证激活码。
        ///// </summary>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes.
        //    // If a user enters incorrect codes for a specified amount of time then the user account
        //    // will be locked out for a specified amount of time.
        //    var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
        //    if (result.Succeeded)
        //    {
        //        return Redirect(model.ReturnUrl);
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        _logger.LogWarning(7, "User account locked out.");
        //        return View("Lockout");
        //    }
        //    ModelState.AddModelError(string.Empty, "Invalid code.");
        //    return View(model);
        //}
    }
}