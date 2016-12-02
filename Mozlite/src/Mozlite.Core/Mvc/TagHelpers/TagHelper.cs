using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers
{
    /// <summary>
    /// 标记基类。
    /// </summary>
    public abstract class TagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
        }

        /// <summary>
        /// 初始化当前标签上下文。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        public override void Init(TagHelperContext context)
        {
        }

#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            Process(context, output);
        }
    }

    /// <summary>
    /// 可访问<see cref="ViewContext"/>实例的标记。
    /// </summary>
    public abstract class ViewContextableTagHelper : TagHelper
    {
        /// <summary>
        /// 试图上下文。
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// HTTP上下文实例。
        /// </summary>
        protected HttpContext HttpContext => ViewContext.HttpContext;
    }
}