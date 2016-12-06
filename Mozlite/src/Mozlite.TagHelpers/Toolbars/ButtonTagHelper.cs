using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.TagHelpers.Toolbars
{
    /// <summary>
    /// 按钮工具栏。
    /// </summary>
    [HtmlTargetElement("buttonbar", ParentTag = "toolbar")]
    public class ButtonTagHelper : TagHelper
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
            output.MergeClassNames(context, "bar");
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }
}