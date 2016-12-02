using Mozlite.Core;

namespace Mozlite.Extensions.Follows
{
    /// <summary>
    /// 收藏管理接口。
    /// </summary>
    public interface IFollowManager : ISingletonService
    {
        /// <summary>
        /// 添加收藏。
        /// </summary>
        /// <typeparam name="TTarget">对象模型类型。</typeparam>
        /// <param name="follow">收藏实例。</param>
        /// <returns>返回添加结果。</returns>
        bool AddFollow<TTarget>(Follow follow)
            where TTarget : IFollowable;
    }
}