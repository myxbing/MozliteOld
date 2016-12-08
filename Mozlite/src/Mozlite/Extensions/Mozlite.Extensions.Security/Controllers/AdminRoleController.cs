using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 用户组控制器。
    /// </summary>
    public class AdminRoleController : AdminControllerBase
    {
        private readonly IRoleManager _roleManager;
        public AdminRoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Load());
        }

        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
                return View();
            return View(await _roleManager.FindByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Role role)
        {
            var edit = role.RoleId > 0;
            var result = edit ? await _roleManager.UpdateAsync(role):await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return Success($"你已经成功{(edit ? "更新" : "添加")}了用户组“{role.DisplayName}”!");
            var error = result.Errors.FirstOrDefault();
            if (error != null)
                return Error(Resources.ResourceManager.GetString(error.Code));
            return Error("添加用户组失败!");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string ids)
        {
            if (await _roleManager.DeleteAsync(ids))
                return Success("你已经成功删除所选择的用户组！");
            return Error("删除用户组失败！");
        }
    }
}