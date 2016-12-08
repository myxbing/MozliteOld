using System;

namespace Mozlite.TagHelpers.ListViews
{
    /// <summary>
    /// 列模式。
    /// </summary>
    [Flags]
    public enum ItemMode
    {
        /// <summary>
        /// 正常。
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 首列。
        /// </summary>
        First = 1,
        /// <summary>
        /// 尾列。
        /// </summary>
        Last = 2,
        /// <summary>
        /// 整行。
        /// </summary>
        Line = First | Last,
    }
}