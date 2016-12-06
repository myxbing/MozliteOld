using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

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
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.MergeClassNames(context, "btn-group");
            output.Content.AppendHtml($"<a href=\"javascript:;\" class=\"btn dropdown-toggle\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">{Title} <span class=\"caret\"></span></a>");
            output.Content.AppendHtml("<ul class=\"dropdown-menu\">");
            output.Content.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml("</ul>");
        }
    }
}