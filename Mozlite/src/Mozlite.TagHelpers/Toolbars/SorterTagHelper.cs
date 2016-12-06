using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.Mvc;
using Mozlite.Utils;

namespace Mozlite.TagHelpers.Toolbars
{
    /// <summary>
    /// 排序下拉列表。
    /// </summary>
    [HtmlTargetElement("sortbar", ParentTag = "toolbar")]
    public class SorterTagHelper : ViewContextableTagHelper
    {
        private readonly IEnumLocalizer _localizer;
        /// <summary>
        /// 初始化类<see cref="SorterTagHelper"/>。
        /// </summary>
        /// <param name="localizer">枚举本地化实例。</param>
        public SorterTagHelper(IEnumLocalizer localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// 查询实例。
        /// </summary>
        [HtmlAttributeName("x-query")]
        public ISortable Query { get; set; }

        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Query == null)
            {
                output.SuppressOutput();
                return;
            }
            output.TagName = "div";
            output.MergeClassNames(context, "bar-sorter", "dropdown");
            output.Content.AppendHtml($"<div style=\"cursor: default;\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"><i class=\"fa fa-sort-amount-{(Query.IsDesc ? "desc" : "asc")}\"></i></div>");
            var builder = new TagBuilder("ul");
            builder.AddCssClass("dropdown-menu dropdown-menu-right");
            foreach (Enum value in Enum.GetValues(Query.Sorter.GetType()))
            {
                var li = new TagBuilder("li");
                if (value.ToString() == Query.Sorter.ToString())
                    li.AddCssClass("active");
                var anchor = new TagBuilder("a");
                anchor.InnerHtml.AppendHtml(_localizer[value]);
                anchor.MergeAttribute("href", ViewContext.SetQueryUrl("Sorter", value.ToString()));
                li.InnerHtml.AppendHtml(anchor);
                builder.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(builder);
        }
    }
}