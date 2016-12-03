using System.Collections.Generic;
using Mozlite.Data;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签管理类。
    /// </summary>
    public class TagManager : ITagManager
    {
        private readonly IRepository<Tag> _tags;
        /// <summary>
        /// 初始化类<see cref="TagManager"/>。
        /// </summary>
        /// <param name="tags">标签数据库操作接口。</param>
        protected TagManager(IRepository<Tag> tags)
        {
            _tags = tags;
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
        /// 获取标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回标签实例。</returns>
        public Tag Get(int id)
        {
            return _tags.Find(x => x.Id == id);
        }
    }
}