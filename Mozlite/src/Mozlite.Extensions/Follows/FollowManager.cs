using Mozlite.Data;

namespace Mozlite.Extensions.Follows
{
    /// <summary>
    /// 收藏管理类。
    /// </summary>
    public class FollowManager : IFollowManager
    {
        private readonly IRepository<Follow> _repository;
        /// <summary>
        /// 初始化类<see cref="FollowManager"/>。
        /// </summary>
        /// <param name="repository">数据库操作接口。</param>
        public FollowManager(IRepository<Follow> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 添加收藏。
        /// </summary>
        /// <typeparam name="TTarget">对象模型类型。</typeparam>
        /// <param name="follow">收藏实例。</param>
        /// <returns>返回添加结果。</returns>
        public bool AddFollow<TTarget>(Follow follow) where TTarget : IFollowable
        {
            if (
                _repository.Any(
                    x =>
                        x.TargetId == follow.TargetId && x.UserId == follow.UserId &&
                        x.ExtensionName == follow.ExtensionName))
                return true;
            return _repository.BeginTransaction(db =>
            {
                if (!db.Create(follow))
                    return false;
                return db.As<TTarget>().IncreaseBy(x => x.Id == follow.TargetId, x => x.Follows, 1);
            });
        }
    }
}