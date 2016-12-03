using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mozlite.Data;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签扩展类。
    /// </summary>
    public static class TagExtensions
    {
        /// <summary>
        /// 新建标签的实体对象。
        /// </summary>
        /// <typeparam name="TModel">实体类型。</typeparam>
        /// <typeparam name="TIndexer">标签关联类型。</typeparam>
        /// <param name="db">数据库操作实例。</param>
        /// <param name="model">模型实例对象。</param>
        /// <returns>返回新建结果。</returns>
        public static bool CreateTagable<TModel, TIndexer>(this IRepository<TModel> db, TModel model)
            where TModel : ITagable
            where TIndexer : TagIndexerBase, new()
        {
            model.TagIndexed = true;
            if (string.IsNullOrWhiteSpace(model.Tags))
                return db.Create(model);
            var tags = model.Tags
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
            return db.BeginTransaction(mt =>
            {
                if (!mt.Create(model))
                    return false;
                var tdb = mt.As<Tag>();
                var indexer = mt.As<TIndexer>();
                foreach (var tag in tags)
                {
                    var current = tdb.Find(x => x.Name == tag);
                    if (current == null)
                    {
                        current = new Tag { Name = tag };
                        if (!tdb.Create(current))
                            return false;
                    }
                    if (!indexer.Create(new TIndexer { TagId = current.Id, Id = model.Id }))
                        return false;
                }
                return true;
            });
        }

        /// <summary>
        /// 新建标签的实体对象。
        /// </summary>
        /// <typeparam name="TModel">实体类型。</typeparam>
        /// <typeparam name="TIndexer">标签关联类型。</typeparam>
        /// <param name="db">数据库操作实例。</param>
        /// <param name="model">模型实例对象。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回新建结果。</returns>
        public static async Task<bool> CreateTagableAsync<TModel, TIndexer>(this IRepository<TModel> db, TModel model, CancellationToken cancellationToken = default(CancellationToken))
            where TModel : ITagable
            where TIndexer : TagIndexerBase, new()
        {
            model.TagIndexed = true;
            if (string.IsNullOrWhiteSpace(model.Tags))
                return await db.CreateAsync(model, cancellationToken);
            var tags = model.Tags
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
            return await db.BeginTransactionAsync(async mt =>
            {
                if (!await mt.CreateAsync(model, cancellationToken))
                    return false;
                var tdb = mt.As<Tag>();
                var indexer = mt.As<TIndexer>();
                foreach (var tag in tags)
                {
                    var current = await tdb.FindAsync(x => x.Name == tag, cancellationToken);
                    if (current == null)
                    {
                        current = new Tag { Name = tag };
                        if (!await tdb.CreateAsync(current, cancellationToken))
                            return false;
                    }
                    if (!await indexer.CreateAsync(new TIndexer { TagId = current.Id, Id = model.Id }, cancellationToken))
                        return false;
                }
                return true;
            }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 更新标签的实体对象。
        /// </summary>
        /// <typeparam name="TModel">实体类型。</typeparam>
        /// <typeparam name="TIndexer">标签关联类型。</typeparam>
        /// <param name="db">数据库操作实例。</param>
        /// <param name="model">模型实例对象。</param>
        /// <returns>返回更新结果。</returns>
        public static bool UpdateTagable<TModel, TIndexer>(this IRepository<TModel> db, TModel model)
            where TModel : ITagable
            where TIndexer : TagIndexerBase, new()
        {
            model.TagIndexed = true;
            if (string.IsNullOrWhiteSpace(model.Tags))
                return db.Update(model);
            var tags = model.Tags
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
            return db.BeginTransaction(mt =>
            {
                if (!mt.Update(model))
                    return false;
                var tdb = mt.As<Tag>();
                var indexer = mt.As<TIndexer>();
                indexer.Delete(x => x.Id == model.Id);//清理原有索引
                foreach (var tag in tags)
                {
                    var current = tdb.Find(x => x.Name == tag);
                    if (current == null)
                    {
                        current = new Tag { Name = tag };
                        if (!tdb.Create(current))
                            return false;
                    }
                    if (!indexer.Create(new TIndexer { TagId = current.Id, Id = model.Id }))
                        return false;
                }
                return true;
            });
        }

        /// <summary>
        /// 更新标签的实体对象。
        /// </summary>
        /// <typeparam name="TModel">实体类型。</typeparam>
        /// <typeparam name="TIndexer">标签关联类型。</typeparam>
        /// <param name="db">数据库操作实例。</param>
        /// <param name="model">模型实例对象。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回更新结果。</returns>
        public static async Task<bool> UpdateTagableAsync<TModel, TIndexer>(this IRepository<TModel> db, TModel model, CancellationToken cancellationToken = default(CancellationToken))
            where TModel : ITagable
            where TIndexer : TagIndexerBase, new()
        {
            model.TagIndexed = true;
            if (string.IsNullOrWhiteSpace(model.Tags))
                return await db.UpdateAsync(model, cancellationToken);
            var tags = model.Tags
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
            return await db.BeginTransactionAsync(async mt =>
            {
                if (!await mt.UpdateAsync(model, cancellationToken))
                    return false;
                var tdb = mt.As<Tag>();
                var indexer = mt.As<TIndexer>();
                await indexer.DeleteAsync(x => x.Id == model.Id, cancellationToken);//清理原有索引
                foreach (var tag in tags)
                {
                    var current = await tdb.FindAsync(x => x.Name == tag, cancellationToken);
                    if (current == null)
                    {
                        current = new Tag { Name = tag };
                        if (!await tdb.CreateAsync(current, cancellationToken))
                            return false;
                    }
                    if (!await indexer.CreateAsync(new TIndexer { TagId = current.Id, Id = model.Id }, cancellationToken))
                        return false;
                }
                return true;
            }, cancellationToken: cancellationToken);
        }
    }
}