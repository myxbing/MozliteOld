using System.Threading.Tasks;
using Mozlite.Core;

namespace Mozlite.Extensions.Tasks
{
    /// <summary>
    /// 图片下载器接口。
    /// </summary>
    public interface IImageDownloader : ISingletonServices
    {
        /// <summary>
        /// 下载图片文件。
        /// </summary>
        /// <returns>返回下载文件结果。</returns>
        Task<bool> DownloadAsync();
    }
}