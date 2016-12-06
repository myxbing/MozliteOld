using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.Extensions;

namespace Mozlite.TagHelpers.Common
{
    /// <summary>
    /// 状态标签。
    /// </summary>
    [HtmlTargetElement("status-o", Attributes = AttributeName)]
    public class ObjectStatusTagHelper : TagHelper
    {
        private const string AttributeName = "x-value";

        /// <summary>
        /// 状态。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public ObjectStatus Status { get; set; }

        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("x-status");
            var builder = new TagBuilder("i");
            builder.AddCssClass("fa");
            if (Status == ObjectStatus.Normal)
            {
                builder.AddCssClass("fa-check-square-o");
                wrapper.AddCssClass("text-success");
            }
            else if (Status == ObjectStatus.Disabled)
            {
                builder.AddCssClass("fa-close");
                wrapper.AddCssClass("text-danger");
            }
            else
            {
                builder.AddCssClass("fa-warning");
                wrapper.AddCssClass("text-warning");
            }
            output.TagName = "span";
            output.MergeAttributes(wrapper);
            output.Content.AppendHtml(builder);
            output.Content.AppendHtml(Resources.ResourceManager.GetString($"ObjectStatus_{Status}"));
        }
    }
}