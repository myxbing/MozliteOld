using Mozlite.Core;
using Mozlite.Data;

namespace Mozlite.Extensions.Favorites
{
    /// <summary>
    /// 收藏管理接口。
    /// </summary>
    public interface IFavoriteManager : ISingletonService
    {
        /// <summary>
        /// 添加收藏。
        /// </summary>
        /// <typeparam name="TTarget">对象模型类型。</typeparam>
        /// <param name="favorite">收藏实例。</param>
        /// <returns>返回添加结果。</returns>
        bool AddFavorite<TTarget>(Favorite favorite)
            where TTarget : IFavoritable;
    }

    /// <summary>
    /// 收藏管理类。
    /// </summary>
    public class FavoriteManager : IFavoriteManager
    {
        private readonly IRepository<Favorite> _repository;
        public FavoriteManager(IRepository<Favorite> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 添加收藏。
        /// </summary>
        /// <typeparam name="TTarget">对象模型类型。</typeparam>
        /// <param name="favorite">收藏实例。</param>
        /// <returns>返回添加结果。</returns>
        public bool AddFavorite<TTarget>(Favorite favorite) where TTarget : IFavoritable
        {
            if (
                _repository.Any(
                    x =>
                        x.TargetId == favorite.TargetId && x.UserId == favorite.UserId &&
                        x.ExtensionName == favorite.ExtensionName))
                return true;
            return _repository.BeginTransaction(db =>
            {
                if (!db.Create(favorite))
                    return false;
                return db.As<TTarget>().IncreaseBy(x => x.Id == favorite.TargetId, x => x.Favorites, 1);
            });
        }
    }
}