using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Tags.Controllers
{
    /// <summary>
    /// 标签管理控制器。
    /// </summary>
    public class AdminController : AdminControllerBase
    {
        private readonly ITagManager _tagManager;
        private readonly ICategoryManager _categoryManager;

        public AdminController(ITagManager tagManager, ICategoryManager categoryManager)
        {
            _tagManager = tagManager;
            _categoryManager = categoryManager;
        }

        public IActionResult Index(TagQuery query)
        {
            ViewBag.GetName = new Func<int, string>(id => _categoryManager.GetName(id));
            return View(_tagManager.Load(query));
        }

        public IActionResult Edit(int id = 0, int cid = 0)
        {
            if (id == 0)
                return View(new Tag { CategoryId = cid });
            return View(_tagManager.Get(id));
        }

        [HttpPost]
        public IActionResult Edit(Tag model)
        {
            return Json(_tagManager.Save(model), "标签");
        }

        [HttpPost]
        public IActionResult Delete(string ids)
        {
            return Json(_tagManager.Delete(ids), "标签");
        }

        [HttpPost]
        public async Task<IActionResult> UploadIcon(IFormFile file)
        {
            var url = await _tagManager.UploadIconAsync(file);
            if (url == null)
                return Error("上传图标错误！");
            return Success("你已经成功上传图标！", new { url });
        }

        public IActionResult Body(int id)
        {
            return View(_tagManager.Get(id));
        }

        [HttpPost]
        public IActionResult Body(Tag model)
        {
            return Json(_tagManager.SaveBody(model.Id, model.Body), "标签内容");
        }
    }
}