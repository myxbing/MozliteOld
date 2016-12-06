using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.TagHelpers.Toolbars
{
    /// <summary>
    /// 工具栏。
    /// </summary>
    [HtmlTargetElement("toolbar")]
    public class ToolbarTagHelper : TagHelper
    {
        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            var builder = new TagBuilder("div");
            builder.AddCssClass("toolbar");
            builder.AddCssClass("clearfix");
            builder.MergeAttribute("role", "toolbar");
            output.MergeAttributes(builder);
        }
    }
}