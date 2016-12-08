using System;

namespace Mozlite.Extensions.Security.ViewModels
{
    /// <summary>
    /// 锁定用户。
    /// </summary>
    public class LockoutUser
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        public string UserIds { get; set; }

        /// <summary>
        /// 默认锁定一天。
        /// </summary>
        public DateTimeOffset LockEnd { get; set; } = DateTimeOffset.Now.AddDays(1);
    }
}