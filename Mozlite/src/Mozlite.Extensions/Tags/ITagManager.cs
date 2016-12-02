using System.Collections.Generic;
using Mozlite.Data;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签管理接口。
    /// </summary>
    /// <typeparam name="TTag">标签类。</typeparam>
    /// <typeparam name="TIndexer">索引关联类。</typeparam>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public interface ITagManager<TTag, TIndexer, TModel>
        where TTag : TagBase, new()
        where TIndexer : TagIndexerBase, new()
        where TModel : ITagable, new()
    {
        /// <summary>
        /// 保存标签。
        /// </summary>
        /// <param name="tag">标签实例。</param>
        /// <returns>返回保存结果。</returns>
        DataResult Save(TTag tag);

        /// <summary>
        /// 分页查询模型列表。
        /// </summary>
        /// <typeparam name="TQuery">查询实例类型。</typeparam>
        /// <param name="query">查询实例对象。</param>
        /// <returns>返回查询列表。</returns>
        TQuery Load<TQuery>(TQuery query)
            where TQuery : TagQueryBase<TTag, TIndexer, TModel>;

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="ids">标签Id集合。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(string ids);

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="ids">标签Id集合。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(int[] ids);

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(int id);

        /// <summary>
        /// 获取热门的标签。
        /// </summary>
        /// <param name="size">标签数量。</param>
        /// <returns>返回标签列表。</returns>
        IEnumerable<TTag> Load(int size);

        /// <summary>
        /// 获取标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回标签实例。</returns>
        TTag Get(int id);
    }

    /// <summary>
    /// 标签管理类。
    /// </summary>
    /// <typeparam name="TTag">标签类。</typeparam>
    /// <typeparam name="TIndexer">索引关联类。</typeparam>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public abstract class TagManager<TTag, TIndexer, TModel> : ITagManager<TTag, TIndexer, TModel> where TTag : TagBase, new() where TIndexer : TagIndexerBase, new() where TModel : ITagable, new()
    {
        private readonly IRepository<TTag> _tags;
        private readonly IRepository<TIndexer> _indexers;
        private readonly IRepository<TModel> _db;
        /// <summary>
        /// 初始化类<see cref="TagManager{TTag,TIndexer,TModel}"/>。
        /// </summary>
        /// <param name="tags">标签数据库操作接口。</param>
        /// <param name="indexers">索引数据库操作接口。</param>
        /// <param name="db">模型数据库操作接口。</param>
        protected TagManager(IRepository<TTag> tags, IRepository<TIndexer> indexers, IRepository<TModel> db)
        {
            _tags = tags;
            _indexers = indexers;
            _db = db;
        }

        /// <summary>
        /// 保存标签。
        /// </summary>
        /// <param name="tag">标签实例。</param>
        /// <returns>返回保存结果。</returns>
        public DataResult Save(TTag tag)
        {
            if (_tags.Any(x => x.Name == tag.Name && x.Id != tag.Id))
                return DataAction.Duplicate;
            if (tag.Id > 0)
                return DataResult.FromResult(_tags.Update(tag), DataAction.Updated);
            return DataResult.FromResult(_tags.Create(tag), DataAction.Created);
        }

        /// <summary>
        /// 分页查询模型列表。
        /// </summary>
        /// <typeparam name="TQuery">查询实例类型。</typeparam>
        /// <param name="query">查询实例对象。</param>
        /// <returns>返回查询列表。</returns>
        public TQuery Load<TQuery>(TQuery query) where TQuery : TagQueryBase<TTag, TIndexer, TModel>
        {
            return _db.Load(query);
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
        /// <param name="size">标签数量。</param>
        /// <returns>返回标签列表。</returns>
        public IEnumerable<TTag> Load(int size)
        {
            return _tags.Query.OrderByDescending(x => x.Count).AsEnumerable(size);
        }

        /// <summary>
        /// 获取标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回标签实例。</returns>
        public TTag Get(int id)
        {
            return _tags.Find(x => x.Id == id);
        }
    }
}