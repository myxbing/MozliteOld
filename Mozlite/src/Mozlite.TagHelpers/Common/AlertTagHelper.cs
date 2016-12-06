using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Mozlite.Mvc.Messages;

namespace Mozlite.TagHelpers.Common
{
    /// <summary>
    /// 警告标签。
    /// </summary>
    [HtmlTargetElement("alert")]
    public class AlertTagHelper : ViewContextableTagHelper
    {
        private const string ClosableAttributeName = "x-close";

        /// <summary>
        /// 是否可关闭。
        /// </summary>
        [HtmlAttributeName(ClosableAttributeName)]
        public bool IsClosable { get; set; } = true;

        /// <summary>
        /// 类型。
        /// </summary>
        [HtmlAttributeName("x-type")]
        public string Type { get; set; }

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var message = ViewContext.ViewData["BsMessage"] as BsMessage;
            IHtmlContent content;
            if (!string.IsNullOrWhiteSpace(message?.Message))
                content = new HtmlString(message.Message);
            else
            {
                var tagContent = await output.GetChildContentAsync();
                if (tagContent.IsEmptyOrWhiteSpace)
                {
                    output.SuppressOutput();
                    return;
                }
                content = tagContent;
            }
            var builder = new TagBuilder("div");
            builder.AddCssClass("alert");
            var type = Type ?? message?.Type.ToString();
            if (type != null)
                builder.AddCssClass("alert-" + type.ToLower());
            if (IsClosable)
                builder.AddCssClass("alert-dismissible");

            output.TagName = "div";
            output.MergeAttributes(builder);
            output.Attributes.SetAttribute("style", context.GetAndAppendStyle("display:block;"));

            output.Content.Clear();
            output.Content.AppendHtml("<span class=\"alert-message\">");
            output.Content.AppendHtml(content);
            output.Content.AppendHtml("</span>");
            if (IsClosable)
                output.Content.AppendHtml(
                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>");
        }
    }
}