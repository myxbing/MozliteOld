//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using Mozlite.Mvc.TagHelpers;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Routing;

//namespace Mozlite.Mvc.AdminMenus
//{
//    /// <summary>
//    /// 管理员菜单标签。
//    /// </summary>
//    [HtmlTargetElement("menu", Attributes = AttributeName)]
//    public class AdminMenuTagHelper : ViewContextableTagHelper
//    {
//        private readonly IMenuProviderFactory _menuProviderFactory;
//        private readonly IUrlHelperFactory _factory;
//        private IUrlHelper _urlHelper;
//        private const string AttributeName = "x-name";

//        /// <summary>
//        /// 菜单提供者名称。
//        /// </summary>
//        [HtmlAttributeName(AttributeName)]
//        public string Provider { get; set; }

//        /// <summary>
//        /// 初始化类<see cref="AdminMenuTagHelper"/>。
//        /// </summary>
//        /// <param name="menuProviderFactory">菜单提供者工厂接口。</param>
//        /// <param name="factory">URL辅助类工厂接口。</param>
//        public AdminMenuTagHelper(IMenuProviderFactory menuProviderFactory, IUrlHelperFactory factory)
//        {
//            _menuProviderFactory = menuProviderFactory;
//            _factory = factory;
//        }

//        /// <summary>
//        /// 访问并呈现当前标签实例。
//        /// </summary>
//        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
//        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
//        public override void Process(TagHelperContext context, TagHelperOutput output)
//        {
//            _urlHelper = _factory.GetUrlHelper(ViewContext);
//            output.TagName = "nav";
//            var items = _menuProviderFactory.GetMenus(Provider)
//                .Where(it => it.Level == 0 && ViewContext.IsInRoles(it))//当前项
//                .OrderByDescending(it => it.Priority);
//            if (!items.Any())
//                return;
//            var current = ViewContext.GetCurrent(_menuProviderFactory, Provider, _urlHelper);
//            var builder = SubMenuRender(items, current, false);
//            builder.MergeAttribute("class", "main-menu", true);
//            output.PostContent.AppendHtml(builder);
//        }

//        private TagBuilder SubMenuRender(IEnumerable<MenuItem> items, MenuItem current, bool isCurrent)
//        {
//            var ul = new TagBuilder("ul");
//            ul.AddCssClass("sub-menu");
//            if (isCurrent)
//                ul.AddCssClass("open");
//            foreach (var item in items)
//            {
//                if (!ViewContext.IsInRoles(item))
//                    continue;
//                var li = new TagBuilder("li");
//                var active = IsCurrent(current, item);
//                if (active)
//                    li.AddCssClass("active");
//                var achor = new TagBuilder("a");
//                achor.AddCssClass($"id-{item.Name.Replace('.', '-')}");
//                if (item.Any())
//                    achor.AddCssClass("js-sub-menu-toggle");
//                achor.MergeAttribute("href", item.LinkUrl(_urlHelper), true);
//                string badge = null;
//                if (item.BadgeText != null)
//                    badge = $" <span class=\"badge element-bg-color-{item.BadgeColor}\">{item.BadgeText}</span>";
//                string icon = null;
//                if (item.IconName != null)
//                    icon = $"<i class=\"fa fa-{item.IconName} fa-fw\"></i>";
//                achor.InnerHtml.AppendHtml($"{icon}<span class=\"text\">{item.Text}{badge}</span>");
//                if (item.Any())
//                    achor.InnerHtml.AppendHtml("<i class=\"toggle-icon fa fa-angle-")
//                        .AppendHtml(active ? "down" : "left")
//                        .AppendHtml("\"></i>");
//                li.InnerHtml.AppendHtml(achor);
//                if (item.Any())
//                    li.InnerHtml.AppendHtml(SubMenuRender(item, current, active));
//                ul.InnerHtml.AppendHtml(li);
//            }
//            return ul;
//        }

//        private bool IsCurrent(MenuItem current, MenuItem item)
//        {
//            while (current != null && current.Level >= 0)
//            {
//                if (current.Name == item.Name)
//                    return true;
//                current = current.Parent;
//            }
//            return false;
//        }
//    }
//}