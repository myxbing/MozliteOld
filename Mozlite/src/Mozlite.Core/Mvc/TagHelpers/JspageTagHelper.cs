using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers
{
    /// <summary>
    /// 脚本分页标签。
    /// </summary>
    public class JsPageTagHelper : TagHelper
    {
        private const string BorderAttributeName = "x-border";
        private const string SizeAttributeName = "x-size";
        private const string PageSizeAttributeName = "x-pagesize";
        private const string TargetAttributeName = "x-target";
        /// <summary>
        /// 是否有边框。
        /// </summary>
        [HtmlAttributeName(BorderAttributeName)]
        public bool Border { get; set; }

        /// <summary>
        /// 总记录数。
        /// </summary>
        [HtmlAttributeName(SizeAttributeName)]
        public int Size { get; set; }

        /// <summary>
        /// 每页显示记录数。
        /// </summary>
        [HtmlAttributeName(PageSizeAttributeName)]
        public int PageSize { get; set; }

        /// <summary>
        /// 分页项目的标签。
        /// </summary>
        [HtmlAttributeName(TargetAttributeName)]
        public string Target { get; set; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pages = (int)Math.Ceiling(Size * 1.0 / PageSize);
            if (pages <= 1)
            {
                output.TagName = null;
                return;
            }
            var builder = new TagBuilder("ul");
            builder.AddCssClass("pagination jspagination");
            if (!Border)
                builder.AddCssClass("borderless");
            output.TagName = "ul";
            output.MergeAttributes(builder);
            var li = new TagBuilder("li");
            li.MergeAttribute("onclick", "jspage.goprev(this);");
            li.InnerHtml.AppendHtml("<a href=\"javascript:;\">&lt;</a>");
            output.Content.AppendHtml(li);
            li = new TagBuilder("li");
            li.AddCssClass("cursize");
            li.InnerHtml.Append("1 / " + pages);
            li.MergeAttribute("data-pages", pages.ToString());
            li.MergeAttribute("data-current", "1");
            li.MergeAttribute("data-target", Target);
            output.Content.AppendHtml(li);
            li = new TagBuilder("li");
            li.MergeAttribute("onclick", "jspage.gonext(this);");
            li.InnerHtml.AppendHtml("<a href=\"javascript:;\">&gt;</a>");
            output.Content.AppendHtml(li);
        }
    }
}