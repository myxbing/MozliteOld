using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers.Toolbar
{
    /// <summary>
    /// 操作按钮标签。
    /// </summary>
    [HtmlTargetElement("li", Attributes = AttributeName)]
    [HtmlTargetElement("li", Attributes = ActionAttributeName)]
    [HtmlTargetElement("li", Attributes = ActionConfirmAttributeName)]
    [HtmlTargetElement("li", Attributes = IconAttributeName)]
    [HtmlTargetElement("li", Attributes = TextAttributeName)]
    [HtmlTargetElement("li", Attributes = VisibleAttributeName)]
    [HtmlTargetElement("li", Attributes = GotoAttributeName)]
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
        private const string TextAttributeName = "x-text";
        private const string VisibleAttributeName = "x-visible";
        private const string GotoAttributeName = "x-goto";

        /// <summary>
        /// 载入选中窗体地址。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public string Url { get; set; }

        /// <summary>
        /// 转向URL地址。
        /// </summary>
        [HtmlAttributeName(GotoAttributeName)]
        public string Goto { get; set; }

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
        /// 文字。
        /// </summary>
        [HtmlAttributeName(TextAttributeName)]
        public string Text { get; set; }

        /// <summary>
        /// 显示规则。
        /// 整数(1)表示等于选中个数(1)；
        /// 负数（-1）表示小于选中个数(1)；
        /// 正数（+1）表示大于选中个数(1)；
        /// </summary>
        [HtmlAttributeName(VisibleAttributeName)]
        public string Visible { get; set; }

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrWhiteSpace(Visible))
                output.Attributes.Add("_visible", Visible);
            var button = new TagBuilder("button");
            button.MergeAttribute("type", "button");
            if (!string.IsNullOrEmpty(Goto))
                button.MergeAttribute("_goto", Goto);
            else if (!string.IsNullOrWhiteSpace(Url))
                button.MergeAttribute("_modal", Url);
            else if (!string.IsNullOrWhiteSpace(Action)) { 
                button.MergeAttribute("_action", Action);
                if(!string.IsNullOrWhiteSpace(Confirm))
                    button.MergeAttribute("_confirm", Confirm);
            }
            button.AddCssClass("btn");
            if (!string.IsNullOrWhiteSpace(Icon))
                button.InnerHtml.AppendHtml($"<i class=\"fa fa-{Icon.Trim()}\"></i> ");
            if (!string.IsNullOrWhiteSpace(Text))
                button.InnerHtml.AppendHtml(Text);
            else
                button.InnerHtml.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml(button);
        }
    }
}