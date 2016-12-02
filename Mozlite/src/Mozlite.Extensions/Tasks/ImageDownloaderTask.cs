using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mozlite.Core.Tasks;
using Mozlite.Data;
using Mozlite.Data.Metadata;
using Mozlite.FileProviders;

namespace Mozlite.Extensions.Tasks
{
    /// <summary>
    /// 采集器后台服务。
    /// </summary>
    public class ImageDownloaderTask : TaskService
    {
        private readonly ILogger<ImageDownloaderTask> _logger;
        private readonly IEnumerable<IImageDownloader> _downloaders;

        /// <summary>
        /// 初始化类<see cref="ImageDownloaderTask"/>。
        /// </summary>
        /// <param name="logger">日志接口。</param>
        /// <param name="downloaders">下载器列表。</param>
        /// <param name="database">数据库接口。</param>
        /// <param name="sqlHelper">SQL辅助接口。</param>
        /// <param name="model">模型管理接口。</param>
        /// <param name="provider">媒体文件提供者。</param>
        public ImageDownloaderTask(ILogger<ImageDownloaderTask> logger, IEnumerable<IImageDownloader> downloaders, IDatabase database, ISqlHelper sqlHelper, IModel model, IMediaFileProvider provider)
        {
            _logger = logger;
            foreach (var downloader in downloaders)
            {
                var current = downloader as ImageDownloader;
                if (current == null)
                    continue;
                current.Database = database;
                current.SqlHelper = sqlHelper;
                current.Model = model;
                current.Provider = provider;
            }
            _downloaders = downloaders;
        }

        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name => "图片下载器";

        /// <summary>
        /// 描述。
        /// </summary>
        public override string Description => "根据代码程序，会对数据库中的一些图片字段的远程图片下载到本地服务器中。";

        /// <summary>
        /// 执行间隔时间。
        /// </summary>
        public override TaskInterval Interval => 30;

        /// <summary>
        /// 执行方法。
        /// </summary>
        /// <param name="argument">参数。</param>
        public override async Task ExecuteAsync(Argument argument)
        {
            foreach (var downloader in _downloaders)
            {
                await Task.Delay(30 * 1000);
                try
                {
                    await downloader.DownloadAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(3, $"下载器[{downloader.GetType().FullName}]出现错误:{ex.Message}", ex);
                }
            }
        }
    }
}