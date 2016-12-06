using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.TagHelpers.Common
{
    /// <summary>
    /// Switch标签。
    /// </summary>
    public class SwitchTagHelper : ViewContextableTagHelper
    {
        private readonly IHtmlGenerator _generator;
        /// <summary>
        /// 初始化类<see cref="SwitchTagHelper"/>。
        /// </summary>
        /// <param name="generator">HTML辅助接口。</param>
        public SwitchTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        private const string ForAttributeName = "x-for";
        private const string BlankAttributeName = "x-blank";
        private const string AttributeName = "x-name";
        private const string CheckedAttributeName = "x-checked";

        /// <summary>
        /// 是否为Switch。
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }
        /// <summary>
        /// 名称。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public string Name { get; set; }

        /// <summary>
        /// 是否选中。
        /// </summary>
        [HtmlAttributeName(CheckedAttributeName)]
        public bool Checked { get; set; }

        /// <summary>
        /// 是否为空。
        /// </summary>
        [HtmlAttributeName(BlankAttributeName)]
        public bool IsBlank { get; set; }

        /// <inheritdoc/>
        /// <remarks>
        /// Default order is <c>0</c>.
        /// </remarks>
        public override int Order => int.MaxValue;

        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var classes = new List<string> { "onoffswitch" };
            if (IsBlank)
                classes.Add("onoffswitch-blank");
            output.TagName = "div";
            output.MergeClassNames(context, classes.ToArray());
            if (For != null)
            {
                output.Content.AppendHtml(_generator.GenerateCheckBox(ViewContext, For.ModelExplorer, For.Name, null,
                    new { @class = "onoffswitch-checkbox" }));
                output.Content.AppendHtml(
                    $"<label class=\"onoffswitch-label\" for=\"{For.Name}\"><span class=\"onoffswitch-inner\"></span><span class=\"onoffswitch-switch\"></span></label>");
            }
            else
            {
                var builder = new TagBuilder("input");
                builder.MergeAttribute("type", "checkbox");
                if (Checked)
                    builder.MergeAttribute("checked", "checked");
                builder.AddCssClass("onoffswitch-checkbox");
                builder.MergeAttribute("name", Name);
                builder.MergeAttribute("id", Name);
                builder.MergeAttribute("value", "true");
                output.Content.AppendHtml(builder);
                output.Content.AppendHtml(
                    $"<label class=\"onoffswitch-label\" for=\"{Name}\"><span class=\"onoffswitch-inner\"></span><span class=\"onoffswitch-switch\"></span></label>");
            }
        }
    }
}

