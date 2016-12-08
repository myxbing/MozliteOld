using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.TagHelpers;

namespace Mozlite.Extensions.Tags.TagHelpers
{
    /// <summary>
    /// 分类下拉列表框。
    /// </summary>
    [HtmlTargetElement("tagcategorylist")]
    public class TagCategorySelectListTagHelper : SelectableTagHelper
    {
        private readonly ICategoryManager _categoryManager;

        /// <summary>
        /// 初始化类<see cref="SelectableTagHelper"/>。
        /// </summary>
        /// <param name="generator">Html辅助接口。</param>
        /// <param name="categoryManager">分类管理接口。</param>
        public TagCategorySelectListTagHelper(IHtmlGenerator generator, ICategoryManager categoryManager) : base(generator)
        {
            _categoryManager = categoryManager;
        }

        /// <summary>
        /// 添加下拉列表框选项。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="items">下拉列表框列表。</param>
        protected override void Init(List<SelectListItem> items, TagHelperContext context)
        {
            var categories = _categoryManager.LoadCaches();
            var id = context.AllAttributes["value"]?.Value.ToString().AsInt32();
            foreach (var category in categories)
            {
                items.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString(), Selected = id == category.Id });
            }
        }
    }
}