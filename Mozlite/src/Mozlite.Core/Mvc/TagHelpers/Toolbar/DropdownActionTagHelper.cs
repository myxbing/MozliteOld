using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers.Toolbar
{
    /// <summary>
    /// 操作按钮标签。
    /// </summary>
    [HtmlTargetElement("li", Attributes = AttributeName)]
    public class DropdownActionTagHelper : TagHelper
    {
        private const string AttributeName = "x-dropdown";

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
            output.MergeClassNames(context, "top-menu-more");
            output.Content.AppendHtml($"<div class=\"btn-group action\"><button type=\"button\" class=\"btn dropdown-toggle\" data-toggle=\"dropdown\"><i class=\"fa fa-list\"></i> {Title} <span class=\"caret\"></span></button>");
            output.Content.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml("</div>");
            
        }
    }
}