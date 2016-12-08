using Mozlite.Mvc.AdminMenus;

namespace Mozlite.Extensions.Tags
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
            root.AddMenu("contents", menu => menu.Texted("内容", priority: 2).Href("Index", "Admin", TagSettings.ExtensionName)
                .AddMenu("tags", item => item.Texted("所有标签", "tags").Href("Index", "Admin", TagSettings.ExtensionName)
                    .AddMenu("categories", it => it.Texted("标签分类").Href("Index", "AdminCategory", TagSettings.ExtensionName))
                )
            );
        }
    }
}