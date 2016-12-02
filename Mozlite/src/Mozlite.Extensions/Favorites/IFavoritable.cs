namespace Mozlite.Extensions.Favorites
{
    /// <summary>
    /// 可收藏的对象。
    /// </summary>
    public interface IFavoritable
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 收藏数量。
        /// </summary>
        int Favorites { get; set; }
    }
}