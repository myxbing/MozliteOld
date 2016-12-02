using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers
{
    /// <summary>
    /// CheckBox。
    /// </summary>
    [HtmlTargetElement("checkbox")]
    public class CheckBoxTagHelper : ViewContextableTagHelper
    {
        private readonly IHtmlGenerator _generator;
        /// <summary>
        /// 初始化类<see cref="CheckBoxTagHelper"/>。
        /// </summary>
        /// <param name="generator">HTML辅助接口。</param>
        public CheckBoxTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        private const string AttributeName = "x-for";
        private const string NameAttributeName = "x-name";
        private const string ValueAttributeName = "x-value";
        /// <summary>
        /// 模型读取。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [HtmlAttributeName(NameAttributeName)]
        public string Name { get; set; }

        /// <summary>
        /// 值。
        /// </summary>
        [HtmlAttributeName(ValueAttributeName)]
        public string Value { get; set; }

        /// <inheritdoc/>
        /// <remarks>
        /// Default order is <c>0</c>.
        /// </remarks>
        public override int Order => int.MaxValue;

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Clear();
            output.TagName = "label";
            output.MergeClassNames(context, "fancy-checkbox");
            if (For != null)
                output.Content.AppendHtml(_generator.GenerateCheckBox(ViewContext, For.ModelExplorer, For.Name, null,
                    null));
            else
            {
                var value = Value;
                if (string.IsNullOrWhiteSpace(value))
                    value = null;
                else
                    value = $" value=\"{value}\"";
                string name = null;
                if (Name != null)
                    name = $" name=\"{Name}\"";
                output.Content.AppendHtml($"<input type=\"checkbox\" {value}{name}/>");
            }
            output.Content.AppendHtml("<span>");
            var content = await output.GetChildContentAsync();
            if (content.IsEmptyOrWhiteSpace)
                output.Content.AppendHtml("&nbsp;");
            else
                output.Content.AppendHtml(content);
            output.Content.AppendHtml("</span>");
        }
    }
}