using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.TagHelpers.Toolbars;

namespace Mozlite.TagHelpers.Toolbar
{
    /// <summary>
    /// 操作按钮标签。
    /// </summary>
    [HtmlTargetElement("dropdownlist", Attributes = AttributeName, ParentTag = "buttonbar")]
    public class DropdownActionTagHelper : TagHelper
    {
        private const string AttributeName = "x-title";

        /// <summary>
        /// 下拉菜单标题。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public string Title { get; set; }

        /// <summary>
        /// 激活条件。
        /// </summary>
        [HtmlAttributeName("x-enabled")]
        public EnabledMode Enabled { get; set; }

        /// <summary>
        /// 排除Id。
        /// </summary>
        [HtmlAttributeName("x-excluded")]
        public string ExcludeIds { get; set; }

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.MergeClassNames(context, "btn-group");
            var anchor = new TagBuilder("a");
            anchor.MergeAttribute("href", "javascript:;");
            anchor.MergeAttribute("class", "btn dropdown-toggle");
            anchor.MergeAttribute("data-toggle", "dropdown");
            anchor.MergeAttribute("aria-haspopup", "true");
            anchor.MergeAttribute("aria-expanded", "false");
            anchor.InnerHtml.AppendHtml($"{Title} <span class=\"caret\"></span>");
            if (!string.IsNullOrWhiteSpace(ExcludeIds))
                anchor.MergeAttribute("js-excluded", $",{ExcludeIds.Trim()},");
            if (Enabled == EnabledMode.Multi)
                anchor.MergeAttribute("js-enabled", "1+");
            else if (Enabled == EnabledMode.Single)
                anchor.MergeAttribute("js-enabled", "1");
            output.Content.AppendHtml(anchor);
            output.Content.AppendHtml("<ul class=\"dropdown-menu\">");
            output.Content.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml("</ul>");
        }
    }
}