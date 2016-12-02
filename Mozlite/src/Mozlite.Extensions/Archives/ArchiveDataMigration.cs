using Mozlite.Data.Migrations;
using Mozlite.Data.Migrations.Builders;

namespace Mozlite.Extensions.Archives
{
    /// <summary>
    /// 新闻数据库迁移类。
    /// </summary>
    public class ArchiveDataMigration : ObjectDataMigration<Archive>
    {
        /// <summary>
        /// 添加模型对象。
        /// </summary>
        /// <param name="table">添加表格的构建实例对象。</param>
        protected override void Create(CreateTableBuilder<Archive> table)
        {
            table.Column(x => x.Title)
            .Column(x => x.SubTitle)
            .Column(x => x.HLogo)
            .Column(x => x.VLogo)
            .Column(x => x.Description)
            .Column(x => x.CommentId)
            .Column(x => x.Comments)
            .Column(x => x.EnabledComment)
            .Column(x => x.Views)
            .Column(x => x.TodayViews)
            .Column(x => x.WeekViews)
            .Column(x => x.MonthViews)
            .Column(x => x.VScore)
            .Column(x => x.Voters)
            .Column(x => x.Body)
            .Column(x => x.TagIndexed)
            .Column(x => x.Tags)
            .Column(x => x.Source)
            .Column(x => x.SourceTitle)
            .Column(x => x.SourceUrl)
            .Column(x => x.Author)
            .Column(x => x.AuthorId)
            .Column(x => x.Favorites);
        }

        public void Up1(MigrationBuilder builder)
        {
            builder.AddColumn<Archive>(x => x.CategoryId);
            builder.AddColumn<Archive>(x => x.TargetId);
        }

        public void Down1(MigrationBuilder builder)
        {
            builder.DropColumn<Archive>(x => x.CategoryId);
            builder.DropColumn<Archive>(x => x.TargetId);
        }

        public void Up2(MigrationBuilder builder)
        {
            builder.AddColumn<Archive>(x => x.IsSearchIndexed);
        }

        public void Down2(MigrationBuilder builder)
        {
            builder.DropColumn<Archive>(x => x.IsSearchIndexed);
        }
    }
}