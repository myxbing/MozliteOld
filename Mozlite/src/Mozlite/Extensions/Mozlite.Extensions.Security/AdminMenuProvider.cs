using Mozlite.Mvc.AdminMenus;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 后台菜单。
    /// </summary>
    public class AdminMenuProvider : MenuProvider
    {
        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("users", menu => menu.Texted("用户", priority:1).Href("Index", "Admin", SecuritySettings.ExtensionName)
                .AddMenu("index", item => item.Texted("所有用户", "users").Href("Index", "Admin", SecuritySettings.ExtensionName))
            );
        }
    }
}