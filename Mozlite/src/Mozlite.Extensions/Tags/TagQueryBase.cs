using Mozlite.Data;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签分页查询基类。
    /// </summary>
    /// <typeparam name="TTag">标签类。</typeparam>
    /// <typeparam name="TIndexer">索引关联类。</typeparam>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public abstract class TagQueryBase<TTag, TIndexer, TModel> : QueryBase<TModel>
        where TTag : TagBase, new()
        where TIndexer : TagIndexerBase, new()
        where TModel : ITagable, new()
    {
        /// <summary>
        /// 标签名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 初始化查询上下文。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected override void Init(IQueryContext<TModel> context)
        {
            if (!string.IsNullOrWhiteSpace(Name))
                context.InnerJoin<TIndexer>((m, x) => m.Id == x.ModelId)
                    .InnerJoin<TIndexer, TTag>((i, x) => i.TagId == x.Id)
                    .Where<TTag>(x => x.Name == Name);
        }
    }
}