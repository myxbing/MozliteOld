using Mozlite.Data.Migrations;

namespace Mozlite.Extensions.Comments
{
    /// <summary>
    /// 评论数据迁移类。
    /// </summary>
    public class CommentDataMigration : DataMigration<Comment>
    {
        /// <summary>
        /// 当模型建立时候构建的表格实例。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        protected override void Create(MigrationBuilder<Comment> builder)
        {
            builder.CreateTable(t => t.Column(x => x.Id)
                .Column(x => x.Title)
                .Column(x => x.ExtensionName)
                .Column(x => x.TargetId)
                .Column(x => x.TargetUrl));

            builder.CreateTable<Post>(t => t.Column(x => x.Id)
                .Column(x => x.CommentId)
                .Column(x => x.Status)
                .Column(x => x.Avatar)
                .Column(x => x.Body)
                .Column(x => x.Replies)
                .Column(x => x.CreatedDate)
                .Column(x => x.Inform)
                .Column(x => x.Votes)
                .Column(x => x.UserId)
                .Column(x => x.UserName)
                .Column(x => x.ParentId)
                .Column(x => x.IP)
                .ForeignKey<Comment>(x => x.CommentId, x => x.Id, onDelete: ReferentialAction.Cascade)
            );

            builder.CreateIndex(x => new {x.ExtensionName, x.TargetId}, true);
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="builder">迁移实例对象。</param>
        public override void Destroy(MigrationBuilder builder)
        {
            builder.DropTable<Post>();
            base.Destroy(builder);
        }
    }
}