using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mozlite.Data;
using Mozlite.FileProviders;

namespace Mozlite.Extensions.Tags.Data
{
    /// <summary>
    /// 标签管理类。
    /// </summary>
    public class TagManager : ITagManager
    {
        private readonly IRepository<Tag> _tags;
        private readonly IMediaFileProvider _provider;

        /// <summary>
        /// 初始化类<see cref="TagManager"/>。
        /// </summary>
        /// <param name="tags">标签数据库操作接口。</param>
        /// <param name="provider">媒体文件提供者。</param>
        public TagManager(IRepository<Tag> tags, IMediaFileProvider provider)
        {
            _tags = tags;
            _provider = provider;
        }

        /// <summary>
        /// 保存标签。
        /// </summary>
        /// <param name="tag">标签实例。</param>
        /// <returns>返回保存结果。</returns>
        public DataResult Save(Tag tag)
        {
            if (_tags.Any(x => x.Name == tag.Name && x.Id != tag.Id))
                return DataAction.Duplicate;
            if (tag.Id > 0)
                return DataResult.FromResult(_tags.Update(tag), DataAction.Updated);
            return DataResult.FromResult(_tags.Create(tag), DataAction.Created);
        }

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="ids">标签Id集合。</param>
        /// <returns>返回删除结果。</returns>
        public DataResult Delete(string ids)
        {
            return Delete(ids.SplitToInt32());
        }

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="ids">标签Id集合。</param>
        /// <returns>返回删除结果。</returns>
        public DataResult Delete(int[] ids)
        {
            return DataResult.FromResult(_tags.Delete(x => x.Id.Included(ids)), DataAction.Deleted);
        }

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回删除结果。</returns>
        public DataResult Delete(int id)
        {
            return DataResult.FromResult(_tags.Delete(x => x.Id == id), DataAction.Deleted);
        }

        /// <summary>
        /// 获取热门的标签。
        /// </summary>
        /// <param name="categoryId">分类Id。</param>
        /// <returns>返回标签列表。</returns>
        public IEnumerable<Tag> Load(int categoryId)
        {
            return _tags.Load(x => x.CategoryId == categoryId);
        }

        /// <summary>
        /// 分页加载标签。
        /// </summary>
        /// <typeparam name="TQuery">查询实例类型。</typeparam>
        /// <param name="query">查询实例。</param>
        /// <returns>返回分页标签列表。</returns>
        public TQuery Load<TQuery>(TQuery query) where TQuery : QueryBase<Tag>
        {
            return _tags.Load(query);
        }

        /// <summary>
        /// 获取标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回标签实例。</returns>
        public Tag Get(int id)
        {
            return _tags.Find(x => x.Id == id);
        }

        /// <summary>
        /// 保存内容详情。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <param name="body">内容详情。</param>
        /// <returns>返回保存结果。</returns>
        public DataResult SaveBody(int id, string body)
        {
            return DataResult.FromResult(_tags.Update(x=>x.Id == id, new {body}), DataAction.Updated);
        }

        /// <summary>
        /// 上传图标并返回图标地址。
        /// </summary>
        /// <param name="file">文件实例。</param>
        /// <returns>返回图标地址。</returns>
        public async Task<string> UploadIconAsync(IFormFile file)
        {
           return await _provider.UploadAsync(file, true, TagSettings.ExtensionName);
        }
    }
}