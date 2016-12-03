using System;
using Mozlite.Core;
using Mozlite.Data;
using Mozlite.Extensions.Tags;

namespace Mozlite.Extensions.Archives
{
    /// <summary>
    /// 文档管理接口。
    /// </summary>
    public interface IArchiveManager : IObjectManager<Archive>, ISingletonService
    {
        /// <summary>
        /// 分页加载文档列表。
        /// </summary>
        /// <param name="query">文档列表查询实例。</param>
        /// <returns>返回文档列表。</returns>
        ArchiveQuery Load(ArchiveQuery query);
    }

    /// <summary>
    /// 文档管理类。
    /// </summary>
    public class ArchiveManager : ObjectManager<Archive>, IArchiveManager
    {
        /// <summary>
        /// 初始化类<see cref="ArchiveManager"/>。
        /// </summary>
        /// <param name="repository">数据库操作接口。</param>
        public ArchiveManager(IRepository<Archive> repository) : base(repository)
        {
        }

        /// <summary>
        /// 分页加载文档列表。
        /// </summary>
        /// <param name="query">文档列表查询实例。</param>
        /// <returns>返回文档列表。</returns>
        public ArchiveQuery Load(ArchiveQuery query)
        {
            return Database.Load(query);
        }

        /// <summary>
        /// 保存实例。
        /// </summary>
        /// <param name="model">模型实例对象。</param>
        /// <returns>返回执行结果。</returns>
        public override DataResult Save(Archive model)
        {
            if (IsDulicate(model))
                return DataAction.Duplicate;
            if (model.Id > 0)
            {
                model.UpdatedDate = DateTime.Now;
                return DataResult.FromResult(Database.UpdateTagable<Archive, ArchiveTagIndexer>(model), DataAction.Updated);
            }
            return DataResult.FromResult(Database.CreateTagable<Archive, ArchiveTagIndexer>(model), DataAction.Created);
        }
    }
}