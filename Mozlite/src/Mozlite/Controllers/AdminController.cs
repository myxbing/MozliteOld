using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Messages;
using Mozlite.Extensions.Settings;
using Mozlite.Mvc.Controllers;

namespace Mozlite.Controllers
{
    /// <summary>
    /// 后台管理控制器。
    /// </summary>
    public class AdminController : AdminControllerBase
    {
        private readonly ISettingsManager _settingsManager;
        public AdminController(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View(_settingsManager.GetSettings<SiteSettings>());
        }

        [HttpPost]
        public IActionResult Settings(SiteSettings settings)
        {
            if (_settingsManager.SaveSettings(settings))
                return SuccessView("你已经成功更新了网站配置！", settings);
            return SuccessView("更新网站配置失败，请重试！", settings);
        }

        public IActionResult EmailSettings()
        {
            return View(_settingsManager.GetSettings<EmailSettings>());
        }

        [HttpPost]
        public IActionResult EmailSettings(EmailSettings settings)
        {
            if (_settingsManager.SaveSettings(settings))
                return SuccessView("你已经成功更新了邮件配置！", settings);
            return SuccessView("更新邮件配置失败，请重试！", settings);
        }
    }
}