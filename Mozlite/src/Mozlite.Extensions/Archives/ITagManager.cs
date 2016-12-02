using Mozlite.Core;
using Mozlite.Data;
using Mozlite.Extensions.Tags;

namespace Mozlite.Extensions.Archives
{
    /// <summary>
    /// 文档标签管理接口。
    /// </summary>
    public interface ITagManager : ITagManager<ArchiveTag, ArchiveTagIndexer, Archive>, ISingletonService
    {

    }

    /// <summary>
    /// 文档标签管理实现类。
    /// </summary>
    public class TagManager : TagManager<ArchiveTag, ArchiveTagIndexer, Archive>, ITagManager
    {
        /// <summary>
        /// 初始化类<see cref="TagManager"/>。
        /// </summary>
        /// <param name="tags">标签数据库操作接口。</param>
        /// <param name="indexers">索引数据库操作接口。</param>
        /// <param name="db">模型数据库操作接口。</param>
        public TagManager(IRepository<ArchiveTag> tags, IRepository<ArchiveTagIndexer> indexers, IRepository<Archive> db) : base(tags, indexers, db)
        {
        }
    }
}