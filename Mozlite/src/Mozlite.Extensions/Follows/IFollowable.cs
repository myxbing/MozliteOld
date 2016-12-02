namespace Mozlite.Extensions.Follows
{
    /// <summary>
    /// 可关注的对象。
    /// </summary>
    public interface IFollowable
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 收藏数量。
        /// </summary>
        int Follows { get; set; }
    }
}