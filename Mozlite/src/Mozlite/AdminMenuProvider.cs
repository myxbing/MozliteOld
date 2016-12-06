using Mozlite.Mvc.AdminMenus;

namespace Mozlite
{
    /// <summary>
    /// 后台管理菜单。
    /// </summary>
    public class AdminMenuProvider : MenuProvider
    {
        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("home",
                menu => menu.Texted("系统").Href("index", "admin", null)
                        .AddMenu("settings", item => item.Texted("网站配置", "cog").Href("settings", "admin", null))
                        .AddMenu("emailsettings", item => item.Texted("电子邮件配置", "envelope-o").Href("emailsettings", "admin", null))
           );
        }
    }
}