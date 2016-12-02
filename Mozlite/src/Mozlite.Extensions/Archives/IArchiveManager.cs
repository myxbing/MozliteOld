using Mozlite.Core;
using Mozlite.Data;

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
        private readonly ITagManager _tagManager;

        /// <summary>
        /// 初始化类<see cref="ArchiveManager"/>。
        /// </summary>
        /// <param name="repository">数据库操作接口。</param>
        /// <param name="tagManager">标签管理接口。</param>
        public ArchiveManager(IRepository<Archive> repository, ITagManager tagManager) : base(repository)
        {
            _tagManager = tagManager;
        }

        /// <summary>
        /// 分页加载文档列表。
        /// </summary>
        /// <param name="query">文档列表查询实例。</param>
        /// <returns>返回文档列表。</returns>
        public ArchiveQuery Load(ArchiveQuery query)
        {
            return _tagManager.Load(query);
        }
    }
}