using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.TagHelpers.Toolbars;
using Mozlite.Utils;

namespace Mozlite.TagHelpers.ListViews
{
    /// <summary>
    /// 列表头部。
    /// </summary>
    [HtmlTargetElement("item", ParentTag = "list-header")]
    public class ListHeaderItemTagHelper : ViewContextableTagHelper
    {
        [HtmlAttributeName("x-mode")]
        public ItemMode Mode { get; set; }

        [HtmlAttributeName("x-sorter")]
        public object Sorter { get; set; }

        [HtmlAttributeName("x-query")]
        public ISortable Query { get; set; }

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            var builder = new TagBuilder("li");
            builder.AddCssClass("col");
            if ((Mode & ItemMode.First)==ItemMode.First)
            {
                builder.AddCssClass("first-col");
                output.Content.AppendHtml("<input type=\"checkbox\" />");//全选
            }
            if ((Mode & ItemMode.Last) == ItemMode.Last)
            {
                builder.AddCssClass("last-col");
            }
            output.Content.AppendHtml("<span class=\"text\">");
            output.Content.AppendHtml(await output.GetChildContentAsync());
            output.Content.AppendHtml("</span>");
            if (Sorter != null)//排序
            {
                var query = Query ?? ViewContext.ViewData.Model as ISortable;
                if (query != null)
                {
                    output.Content.AppendHtml($"<span class=\"fa fa-long-arrow-{(query.IsDesc ? "down" : "up")}\"></span>");
                    builder.AddCssClass("sortable");
                    if (query.Sorter.ToString() == Sorter.ToString())
                    {
                        builder.AddCssClass("active");
                        builder.MergeAttribute("_href", ViewContext.SetQueryUrl(qs =>
                        {
                            qs["Sorter"] = Sorter;
                            qs["IsDesc"] = !query.IsDesc;
                        }));
                    }
                    else
                        builder.MergeAttribute("_href", ViewContext.SetQueryUrl("Sorter", Sorter));
                }
            }
            output.MergeAttributes(builder);
        }
    }
}