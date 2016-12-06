using System;
using Mozlite.Data;
using Mozlite.TagHelpers.Toolbars;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 用户查询实例。
    /// </summary>
    public class UserQuery : QueryBase, ISortable
    {
        /// <summary>
        /// 是否为降序。
        /// </summary>
        public bool IsDesc { get; set; }

        /// <summary>
        /// 排序实例。
        /// </summary>
        public UserSorter Sorter { get; set; } = UserSorter.CreatedDate;

        Enum ISortable.Sorter => Sorter;

        /// <summary>
        /// 初始化查询上下文。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected override void Init(IQueryContext<User> context)
        {
            base.Init(context);
            switch (Sorter)
            {
                case UserSorter.Name:
                    context.OrderBy(x => x.NormalizedUserName, IsDesc);
                    break;
                case UserSorter.LastLoginDate:
                    context.OrderBy(x => x.LastLoginDate, IsDesc);
                    break;
                default:
                    context.OrderBy(x => x.UserId, IsDesc);
                    break;
            }
        }
    }
}