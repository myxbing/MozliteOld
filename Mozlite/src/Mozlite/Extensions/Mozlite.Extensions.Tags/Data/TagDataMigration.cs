using Mozlite.Data.Migrations;

namespace Mozlite.Extensions.Tags.Data
{
    /// <summary>
    /// 标签数据库迁移类。
    /// </summary>
    /// <typeparam name="TIndexer">索引关联类。</typeparam>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public abstract class TagDataMigration<TIndexer, TModel> : DataMigration<TModel>
        where TIndexer : TagIndexerBase, new()
        where TModel : ITagable, new()
    {
        /// <summary>
        /// 优先级，在两个迁移数据需要先后时候使用。
        /// </summary>
        public override int Priority => 0;

        /// <summary>
        /// 当模型建立时候构建的表格实例。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        public override void Create(MigrationBuilder builder)
        {
            base.Create(builder);
            builder.CreateTable<TIndexer>(table => table.Column(x => x.TagId).Column(x => x.Id)
                .ForeignKey<TModel>(x => x.Id, x => x.Id, onDelete: ReferentialAction.Cascade)
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

    /// <summary>
    /// 标签数据库迁移类。
    /// </summary>
    /// <typeparam name="TIndexer">索引关联类。</typeparam>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public abstract class ObjectTagDataMigration<TIndexer, TModel> : ObjectDataMigration<TModel>
        where TIndexer : TagIndexerBase, new()
        where TModel : ExtendObjectBase, ITagable, new()
    {
        /// <summary>
        /// 优先级，在两个迁移数据需要先后时候使用。
        /// </summary>
        public override int Priority => 0;

        /// <summary>
        /// 当模型建立时候构建的表格实例。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        public override void Create(MigrationBuilder builder)
        {
            base.Create(builder);
            builder.CreateTable<TIndexer>(table => table.Column(x => x.TagId).Column(x => x.Id)
                .ForeignKey<TModel>(x => x.Id, x => x.Id, onDelete: ReferentialAction.Cascade)
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

    /// <summary>
    /// 标签数据库迁移类。
    /// </summary>
    public class TagDataMigration : DataMigration<Tag>
    {
        /// <summary>
        /// 优先级，在两个迁移数据需要先后时候使用。
        /// </summary>
        public override int Priority => -1;

        /// <summary>
        /// 当模型建立时候构建的表格实例。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        protected override void Create(MigrationBuilder<Tag> builder)
        {
            builder.CreateTable(table => table
                .Column(x => x.Id)
                .Column(x => x.Name)
                .Column(x => x.CategoryId)
                .Column(x => x.Description)
                .Column(x => x.Body)
                .Column(x => x.IconUrl)
                .Column(x => x.Follows)
            );
        }
    }
}