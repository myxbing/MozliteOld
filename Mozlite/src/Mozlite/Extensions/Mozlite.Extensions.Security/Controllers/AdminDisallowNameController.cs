using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Security.DisallowNames;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 非法用户名。
    /// </summary>
    public class AdminDisallowNameController : AdminControllerBase
    {
        private readonly INameManager _nameManager;
        public AdminDisallowNameController(INameManager nameManager)
        {
            _nameManager = nameManager;
        }

        public IActionResult Index(NameQuery query)
        {
            query.PageSize = 50;
            return View(_nameManager.Load(query));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string names)
        {
            if (string.IsNullOrWhiteSpace(names))
                return Warning("请输入非法名称，每行一个或以“,”隔开！");
            return Json(_nameManager.Save(names), "名称");
        }

        [HttpPost]
        public IActionResult Delete(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                return Error("名称不存在！");
            return Json(_nameManager.Delete(ids), "名称");
        }
    }
}