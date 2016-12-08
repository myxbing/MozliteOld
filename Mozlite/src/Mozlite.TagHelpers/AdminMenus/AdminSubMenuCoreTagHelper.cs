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
    [HtmlTargetElement("coresubmenu", Attributes = AttributeName)]
    public class AdminSubMenuCoreTagHelper : ViewContextableTagHelper
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
        /// 初始化类<see cref="AdminSubMenuCoreTagHelper"/>。
        /// </summary>
        /// <param name="menuProviderFactory">菜单提供者工厂接口。</param>
        /// <param name="factory">URL辅助类工厂接口。</param>
        public AdminSubMenuCoreTagHelper(IMenuProviderFactory menuProviderFactory, IUrlHelperFactory factory)
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
            output.TagName = "ul";
            var current = ViewContext.GetCurrent(_menuProviderFactory, Provider, _urlHelper);
            var items = current?.GetTopMenu()
                .Where(it => it.Level > 0 && ViewContext.IsInRoles(it))//当前项
                .OrderByDescending(it => it.Priority);
            if (items == null || !items.Any())
                return;
            foreach (var item in items)
            {
                Render(output, current, item);
            }
        }

        private void Render(TagHelperOutput output, MenuItem current, MenuItem item)
        {
            var anchor = new TagBuilder("a");
            anchor.MergeAttribute("href", item.LinkUrl(_urlHelper));
            anchor.MergeAttribute("title", item.Text);
            var span = new TagBuilder("span");
            span.AddCssClass("text");
            if (!string.IsNullOrWhiteSpace(item.IconName))
            {
                span.InnerHtml.AppendHtml($"<i class=\"fa fa-{item.IconName}\"></i>");
                span.InnerHtml.AppendHtml($"<span>{item.Text}</span>");
            }
            else
            {
                span.InnerHtml.AppendHtml(item.Text);
            }
            anchor.InnerHtml.AppendHtml(span);
            var li = new TagBuilder("li");
            li.AddCssClass("list-item");
            li.AddCssClass($"id-{item.Name.Replace('.', '-')}");
            if (current.Name == item.Name)
                li.AddCssClass("active");
            li.InnerHtml.AppendHtml(anchor);
            output.Content.AppendHtml(li);

            if (item.Any())
            {
                foreach (var it in item)
                {
                    Render(output, current, it);
                }
            }
        }
    }
}