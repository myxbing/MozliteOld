using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Mozlite.Data;
using Mozlite.Data.Metadata;
using Mozlite.FileProviders;

namespace Mozlite.Extensions.Tasks
{
    /// <summary>
    /// 图片下载器基类。
    /// </summary>
    public abstract class ImageDownloader : IImageDownloader
    {
        private string _fieldName;
        /// <summary>
        /// 格式化后的列名称。
        /// </summary>
        protected string DelimiterFieldName => _fieldName ?? (_fieldName = SqlHelper.DelimitIdentifier(FieldName));

        /// <summary>
        /// 图片所在列的名称。
        /// </summary>
        protected abstract string FieldName { get; }

        /// <summary>
        /// 表格名称。
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// 媒体类型。
        /// </summary>
        protected abstract string MediaType { get; }

        /// <summary>
        /// 文件所属目标Id列名。
        /// </summary>
        protected virtual string TargetIdFieldName => "Id";

        private string _targetIdFieldName;
        /// <summary>
        /// 格式化后的文件所属目标Id列名。
        /// </summary>
        protected string DelimiterTargetIdFieldName => _targetIdFieldName ?? (_targetIdFieldName = SqlHelper.DelimitIdentifier(TargetIdFieldName));

        internal IModel Model { get; set; }
        internal ISqlHelper SqlHelper { get; set; }
        internal IDatabase Database { get; set; }
        internal IMediaFileProvider Provider { get; set; }
        /// <summary>
        /// 图片目录Id。
        /// </summary>
        protected virtual int DirectoryId => 0;

        /// <inheritdoc />
        public virtual async Task<bool> DownloadAsync()
        {
            var urls = await GetUrlsAsync();
            if (!urls.Any())
                return true;
            foreach (var url in urls)
            {
                try
                {
                    var newUrl = await Provider.DownloadAsync(url.Url, true, MediaType, url.TargetId, DirectoryId, ".jpg");
                    if (newUrl != null)
                        await UpdateAsync(url.Url, newUrl);
                }
                catch
                {
                    await ErrorAsync(url.Url);
                }
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
            return true;
        }

        private string _getUrlsSql;
        /// <summary>
        /// 获取需要下载地址的SQL语句。
        /// </summary>
        /// <returns>返回需要下载的SQL语句。</returns>
        protected virtual string GetUrlsSql()
        {
            if (_getUrlsSql == null)
                _getUrlsSql =
                    $"SELECT TOP(10) {DelimiterFieldName},{DelimiterTargetIdFieldName} FROM {TableName} WHERE {DelimiterFieldName} LIKE 'http://%' AND TryTimes < 5";
            return _getUrlsSql;
        }
        
        /// <summary>
        /// 加载需要下载的图片地址，地址以“http://”开头。
        /// </summary>
        /// <returns>返回图片地址列表。</returns>
        protected virtual async Task<IEnumerable<UrlData>> GetUrlsAsync()
        {
            var urls = new List<UrlData>();
            using (var reader = await Database.ExecuteReaderAsync(GetUrlsSql()))
            {
                while (await reader.ReadAsync())
                {
                    urls.Add(new UrlData(reader));
                }
            }
            return urls;
        }

        private string _updateSql;
        /// <summary>
        /// 更新地址。
        /// </summary>
        /// <param name="url">原地址。</param>
        /// <param name="newUrl">下载后的本地访问地址。</param>
        /// <returns>返回更新结果。</returns>
        protected virtual Task<bool> UpdateAsync(string url, string newUrl)
        {
            if (_updateSql == null)
                _updateSql =
                    $"UPDATE {TableName} SET {DelimiterFieldName} = {SqlHelper.Parameterized("NewUrl")}, TryTimes = 0 WHERE {DelimiterFieldName} = {SqlHelper.Parameterized("Url")};";
            return Database.ExecuteNonQueryAsync(_updateSql, new { Url = url, NewUrl = newUrl });
        }

        private string _errorSql;
        /// <summary>
        /// 更新下载错误次数。
        /// </summary>
        /// <param name="url">原地址。</param>
        /// <returns>返回更新结果。</returns>
        protected virtual Task<bool> ErrorAsync(string url)
        {
            if (_errorSql == null)
                _errorSql =
                    $"UPDATE {TableName} SET TryTimes = TryTimes + 1 WHERE {DelimiterFieldName} = {SqlHelper.Parameterized("Url")};";
            return Database.ExecuteNonQueryAsync(_errorSql, new { Url = url });
        }

        /// <summary>
        /// URL数据。
        /// </summary>
        protected class UrlData
        {
            /// <summary>
            /// 初始化类<see cref="UrlData"/>。
            /// </summary>
            /// <param name="reader">数据库读取实例对象。</param>
            public UrlData(DbDataReader reader)
            {
                TargetId = reader.GetInt32(1);
                Url = reader.GetString(0);
            }

            /// <summary>
            /// 下载文件目标Id。
            /// </summary>
            public int TargetId { get; }

            /// <summary>
            /// URL地址。
            /// </summary>
            public string Url { get; }
        }
    }

    /// <summary>
    /// 图片下载器基类。
    /// </summary>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public abstract class ImageDownloader<TModel> : ImageDownloader
        where TModel : class, IDownloadable, new()
    {
        private string _tableName;
        /// <summary>
        /// 表格名称。
        /// </summary>
        protected override string TableName => _tableName ?? (_tableName = Model.GetTable(typeof(TModel)).ToString());
    }
}