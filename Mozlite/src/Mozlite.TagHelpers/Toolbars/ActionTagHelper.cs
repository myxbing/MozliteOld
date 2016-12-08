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
        private const string AttributeName = "x-modal";
        private const string ActionAttributeName = "x-action";
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
        /// 排除在外的Id。
        /// </summary>
        [HtmlAttributeName("x-excluded")]
        public string ExcludeIds { get; set; }

        /// <summary>
        /// 警告窗口。
        /// </summary>
        [HtmlAttributeName("x-confirm")]
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
        /// 忽略所选，用于添加按钮。
        /// </summary>
        [HtmlAttributeName("x-ignore")]
        public bool IgnoreChecked { get; set; }

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var anchor = CreateAnchor();
            anchor.AddCssClass("btn");
            output.TagName = "a";
            output.MergeAttributes(anchor);
            if (!string.IsNullOrWhiteSpace(Icon))
                output.Content.AppendHtml($"<i class=\"fa fa-{Icon.Trim()}\"></i> ");
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }

        /// <summary>
        /// 创建链接按钮。
        /// </summary>
        /// <returns>返回链接按钮实例。</returns>
        protected TagBuilder CreateAnchor()
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
            if (!string.IsNullOrWhiteSpace(Confirm))
                anchor.MergeAttribute("js-confirm", Confirm);
            if (!string.IsNullOrWhiteSpace(ExcludeIds))
                anchor.MergeAttribute("js-excluded", $",{ExcludeIds.Trim()},");
            if (IgnoreChecked)
                anchor.MergeAttribute("js-ignore", "checked");
            return anchor;
        }
    }

    /// <summary>
    /// 操作按钮标签。
    /// </summary>
    [HtmlTargetElement("listitem", ParentTag = "dropdownlist")]
    public class DropDownListActionTagHelper : ActionTagHelper
    {
        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var anchor = CreateAnchor();
            anchor.AddCssClass("btn");
            if (!string.IsNullOrWhiteSpace(Icon))
                anchor.InnerHtml.AppendHtml($"<i class=\"fa fa-{Icon.Trim()}\"></i> ");
            anchor.InnerHtml.AppendHtml(await output.GetChildContentAsync());
            output.TagName = "li";
            output.Content.AppendHtml(anchor);
        }
    }
}