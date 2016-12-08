using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mozlite.Mvc.AdminMenus;

namespace Mozlite.TagHelpers.AdminMenus
{
    /// <summary>
    /// 管理员菜单标签。
    /// </summary>
    [HtmlTargetElement("coremenu", Attributes = AttributeName)]
    public class AdminMenuCoreTagHelper : ViewContextableTagHelper
    {
        private readonly IMenuProviderFactory _menuProviderFactory;
        private readonly IUrlHelperFactory _factory;
        private IUrlHelper _urlHelper;
        private const string AttributeName = "x-name";

        /// <summary>
        /// 菜单提供者名称。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public string Provider { get; set; }

        /// <summary>
        /// 初始化类<see cref="AdminMenuCoreTagHelper"/>。
        /// </summary>
        /// <param name="menuProviderFactory">菜单提供者工厂接口。</param>
        /// <param name="factory">URL辅助类工厂接口。</param>
        public AdminMenuCoreTagHelper(IMenuProviderFactory menuProviderFactory, IUrlHelperFactory factory)
        {
            _menuProviderFactory = menuProviderFactory;
            _factory = factory;
        }

        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            _urlHelper = _factory.GetUrlHelper(ViewContext);
            output.TagName = "div";
            var items = _menuProviderFactory.GetMenus(Provider)
                .Where(it => it.Level == 0 && ViewContext.IsInRoles(it))//当前项
                .OrderByDescending(it => it.Priority);
            if (!items.Any())
                return;
            var current = ViewContext.GetCurrent(_menuProviderFactory, Provider, _urlHelper).GetTopMenu();
            foreach (var item in items)
            {
                var anchor = new TagBuilder("a");
                anchor.MergeAttribute("href", item.LinkUrl(_urlHelper));
                anchor.MergeAttribute("title", item.Text);
                anchor.InnerHtml.Append(item.Text);
                var wrapper = new TagBuilder("span");
                wrapper.AddCssClass("link-item");
                wrapper.AddCssClass($"id-{item.Name.Replace('.', '-')}");
                if (current?.Name == item.Name)
                    wrapper.AddCssClass("active");
                wrapper.InnerHtml.AppendHtml(anchor);
                wrapper.InnerHtml.AppendHtml("<span class=\"subscript\"></span>");
                if (!string.IsNullOrWhiteSpace(item.IconName))
                    wrapper.InnerHtml.AppendHtml($"<i class=\"fa fa-{item.IconName}\"></i>");
                output.Content.AppendHtml(wrapper);
            }
        }
    }
}