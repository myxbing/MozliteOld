using Mozlite.Data.Migrations;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签数据库迁移类。
    /// </summary>
    /// <typeparam name="TTag">标签类。</typeparam>
    /// <typeparam name="TIndexer">索引关联类。</typeparam>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public abstract class TagDataMigration<TTag, TIndexer, TModel> : DataMigration<TTag>
        where TTag : TagBase, new()
        where TIndexer : TagIndexerBase, new()
        where TModel : ITagable, new()
    {
        /// <summary>
        /// 优先级，在两个迁移数据需要先后时候使用。
        /// </summary>
        public override int Priority => -1;

        /// <summary>
        /// 当模型建立时候构建的表格实例。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        protected override void Create(MigrationBuilder<TTag> builder)
        {
            builder.CreateTable(table => table.Column(x => x.Id).Column(x => x.Name).Column(x => x.Count));
            builder.CreateIndex(x => x.Name, true);
            builder.CreateTable<TIndexer>(table => table.Column(x => x.TagId).Column(x => x.ModelId)
                .ForeignKey<TTag>(x => x.TagId, x => x.Id, onDelete: ReferentialAction.Cascade)
                .ForeignKey<TModel>(x => x.ModelId, x => x.Id, onDelete: ReferentialAction.Cascade)
            );
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        public override void Destroy(MigrationBuilder builder)
        {
            builder.DropTable<TIndexer>();
            base.Destroy(builder);
        }
    }
}