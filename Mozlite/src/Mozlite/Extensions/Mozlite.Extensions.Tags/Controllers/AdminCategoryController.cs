using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Tags.Controllers
{
    /// <summary>
    /// 标签分类控制器。
    /// </summary>
    public class AdminCategoryController : AdminControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public AdminCategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            return View(_categoryManager.LoadCaches());
        }

        public IActionResult Edit(int id = 0)
        {
            if (id == 0)
                return View();
            return View(_categoryManager.GetCache(id));
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            return Json(_categoryManager.Save(model), "标签分类");
        }

        [HttpPost]
        public IActionResult Delete(string ids)
        {
            return Json(_categoryManager.Delete(ids), "标签分类");
        }
    }
}