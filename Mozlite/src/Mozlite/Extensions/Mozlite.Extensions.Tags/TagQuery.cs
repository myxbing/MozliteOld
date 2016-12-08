using System;
using Mozlite.Data;
using Mozlite.TagHelpers.Toolbars;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签查询实例。
    /// </summary>
    public class TagQuery : QueryBase<Tag>, ISortable
    {
        /// <summary>
        /// 初始化查询上下文。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected override void Init(IQueryContext<Tag> context)
        {
            if (!string.IsNullOrWhiteSpace(Name))
                context.Where(x => x.Name.Contains(Name));
            if (Cid > 0)
                context.Where(x => x.CategoryId == Cid);
            switch (Sorter)
            {
                case TagSorter.Follows:
                    context.OrderBy(x => x.Follows, IsDesc);
                    break;
                default:
                    context.OrderBy(x => x.Name, IsDesc);
                    break;
            }
        }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类Id。
        /// </summary>
        public int Cid { get; set; }

        /// <summary>
        /// 是否为降序。
        /// </summary>
        public bool IsDesc { get; set; }

        /// <summary>
        /// 排序实例。
        /// </summary>
        public TagSorter Sorter { get; set; }

        Enum ISortable.Sorter => Sorter;
    }
}