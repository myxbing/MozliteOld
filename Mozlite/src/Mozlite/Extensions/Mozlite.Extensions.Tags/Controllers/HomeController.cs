using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Tags.Controllers
{
    /// <summary>
    /// 标签控制器。
    /// </summary>
    public class HomeController : ControllerBase
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return Error("标签名称不能为空！");
            return View();
        }
    }
}