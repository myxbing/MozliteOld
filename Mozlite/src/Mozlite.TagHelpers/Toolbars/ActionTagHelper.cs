using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.TagHelpers.Toolbars
{
    /// <summary>
    /// 操作按钮标签。
    /// </summary>
    [HtmlTargetElement("action", ParentTag = "group")]
    public class ActionTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private readonly TextEncoder _encoder;
        /// <summary>
        /// 初始化类<see cref="ActionTagHelper"/>。
        /// </summary>
        public ActionTagHelper()
        {
            _encoder = JavaScriptEncoder.Default;
        }

        private const string AttributeName = "x-modal";
        private const string ActionAttributeName = "x-action";
        private const string ActionConfirmAttributeName = "x-action-confirm";
        private const string IconAttributeName = "x-icon";
        private const string EnabledAttributeName = "x-enabled";
        private const string HrefAttributeName = "x-href";

        /// <summary>
        /// 是否载入窗口。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public string Modal { get; set; }

        /// <summary>
        /// 转向URL地址。
        /// </summary>
        [HtmlAttributeName(HrefAttributeName)]
        public string Href { get; set; }

        /// <summary>
        /// 调用Action。
        /// </summary>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        /// <summary>
        /// 确认描述。
        /// </summary>
        [HtmlAttributeName(ActionConfirmAttributeName)]
        public string Confirm { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        [HtmlAttributeName(IconAttributeName)]
        public string Icon { get; set; }

        /// <summary>
        /// 激活规则。
        /// </summary>
        [HtmlAttributeName(EnabledAttributeName)]
        public EnabledMode Enabled { get; set; }

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var anchor = new TagBuilder("a");
            if (Enabled == EnabledMode.Multi)
                anchor.MergeAttribute("js-enabled", "1+");
            else if (Enabled == EnabledMode.Single)
                anchor.MergeAttribute("js-enabled", "1");
            if (!string.IsNullOrEmpty(Href))
            {
                anchor.MergeAttribute("href", Href);
                anchor.MergeAttribute("js-mode", "nav");
            }
            else if (!string.IsNullOrWhiteSpace(Action))
            {
                anchor.MergeAttribute("js-mode", "action");
                anchor.MergeAttribute("href", Action);
            }
            else if (!string.IsNullOrEmpty(Modal))
            {
                anchor.MergeAttribute("href", Modal);
                anchor.MergeAttribute("js-mode", "modal");
            }
            else
                anchor.MergeAttribute("href", "javascript:;");
            anchor.AddCssClass("btn");
            output.TagName = "a";
            output.MergeAttributes(anchor);
            if (!string.IsNullOrWhiteSpace(Icon))
                output.Content.AppendHtml($"<i class=\"fa fa-{Icon.Trim()}\"></i> ");
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }
}