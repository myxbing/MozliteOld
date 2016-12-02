using Mozlite.Data;
using Mozlite.Extensions.Tags;

namespace Mozlite.Extensions.Archives
{
    /// <summary>
    /// 文档查询类。
    /// </summary>
    public class ArchiveQuery : TagQueryBase<ArchiveTag, ArchiveTagIndexer, Archive>
    {
        /// <summary>
        /// 分类Id。
        /// </summary>
        public int Cid { get; set; }

        /// <summary>
        /// 文档状态。
        /// </summary>
        public ObjectStatus? Status { get; set; }

        /// <summary>
        /// 目标Id。
        /// </summary>
        public int? TargetId { get; set; }

        /// <summary>
        /// 初始化查询上下文。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected override void Init(IQueryContext<Archive> context)
        {
            base.Init(context);
            if (Cid > 0)
                context.Where(x => x.CategoryId == Cid);
            if (TargetId != null)
                context.Where(x => x.TargetId == TargetId);
            if (Status != null)
                context.Where(x => x.Status == Status.Value);
        }
    }
}