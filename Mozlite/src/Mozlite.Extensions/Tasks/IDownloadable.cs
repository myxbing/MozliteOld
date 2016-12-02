namespace Mozlite.Extensions.Tasks
{
    /// <summary>
    /// 包含图片采集的对象实例，每一个表格只能包含一个图片下载器。
    /// </summary>
    public interface IDownloadable
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 已经尝试下载次数，超过次数后就不再进行图片下载。
        /// </summary>
        int TryTimes { get; set; }
    }
}