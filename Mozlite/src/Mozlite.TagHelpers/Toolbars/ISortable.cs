using System;

namespace Mozlite.TagHelpers.Toolbars
{
    /// <summary>
    /// 可排序接口。
    /// </summary>
    public interface ISortable
    {
        /// <summary>
        /// 是否为降序。
        /// </summary>
        bool IsDesc { get; }

        /// <summary>
        /// 排序实例。
        /// </summary>
        Enum Sorter { get; }
    }
}