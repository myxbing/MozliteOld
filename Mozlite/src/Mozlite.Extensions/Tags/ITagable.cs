namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签。
    /// </summary>
    public interface ITagable
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 标签。
        /// </summary>
        string Tags { get; set; }
        
        /// <summary>
        /// 是否索引。
        /// </summary>
        bool TagIndexed { get; set; }
    }
}