using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers
{
    /// <summary>
    /// 警告标签。
    /// </summary>
    [HtmlTargetElement("warning")]
    public class IconAlertTagHelper : TagHelper
    {
        private const string AttributeName = "x-icon";

        /// <summary>
        /// 类型。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public string Icon { get; set; } = "warning";

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var builder = new TagBuilder("div");
            builder.AddCssClass("x-warning");
            builder.AddCssClass("alert");
            builder.AddCssClass("alert-warning");
            output.TagName = "div";
            output.MergeAttributes(builder);
            output.Content.AppendHtml($"<i class=\"fa fa-{Icon}\"></i> ");
            output.Content.AppendHtml("<span class=\"alert-message\">");
            output.Content.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml("</span>");
        }
    }
}