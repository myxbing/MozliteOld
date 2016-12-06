using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.TagHelpers.Toolbars
{
    /// <summary>
    /// 搜索工具栏。
    /// </summary>
    [HtmlTargetElement("searchbar", ParentTag = "toolbar")]
    public class SearchTagHelper : TagHelper
    {
        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.MergeClassNames(context, "bar-search");
            output.Content.AppendHtml(
                "<div class=\"bar-search\"><form class=\"form-horizontal\"><div class=\"input-group\">");
            output.Content.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml("<div class=\"input-group-btn\"><button type=\"submit\" class=\"btn\"><i class=\"fa fa-search\"></i></button></div></div></form></div>");
        }
    }
}
