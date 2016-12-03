using Mozlite.Data.Metadata;
using Mozlite.Extensions.Comments;
using Mozlite.Extensions.Favorites;
using Mozlite.Extensions.Searching;
using Mozlite.Extensions.Tags;
using Mozlite.Extensions.Votes;

namespace Mozlite.Extensions.Archives
{
    /// <summary>
    /// 文档类。
    /// </summary>
    [Table("Archives")]
    public class Archive : ExtendObjectBase, ICommentable, IViewable, IVotable, ITagable, IFavoritable, ISearchable
    {
        /// <summary>
        /// 标题。
        /// </summary>
        [Size(256)]
        public string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        [Size(256)]
        public string SubTitle { get; set; }

        /// <summary>
        /// 宽LOGO。
        /// </summary>
        [Size(256)]
        public string HLogo { get; set; }

        /// <summary>
        /// 高LOGO。
        /// </summary>
        [Size(256)]
        public string VLogo { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        [Size(256)]
        public string Description { get; set; }

        /// <summary>
        /// 评论Id。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int CommentId { get; set; }

        /// <summary>
        /// 评论数量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Comments { get; set; }

        /// <summary>
        /// 是否允许评论。
        /// </summary>
        public bool EnabledComment { get; set; }

        /// <summary>
        /// 总访问量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Views { get; set; }

        /// <summary>
        /// 今日访问量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int TodayViews { get; set; }

        /// <summary>
        /// 星期访问量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int WeekViews { get; set; }

        /// <summary>
        /// 本月访问量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int MonthViews { get; set; }

        /// <summary>
        /// 投票得分。
        /// </summary>
        [Ignore(Ignore.Update)]
        public double VScore { get; set; }

        /// <summary>
        /// 投票人数。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Voters { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        [Ignore(Ignore.List)]
        public string Body { get; set; }

        /// <summary>
        /// 标签。
        /// </summary>
        [Size(256)]
        public string Tags { get; set; }

        /// <summary>
        /// 是否索引。
        /// </summary>
        public bool TagIndexed { get; set; }

        /// <summary>
        /// 来源。
        /// </summary>
        [Size(64)]
        public string Source { get; set; }

        /// <summary>
        /// 来源标题。
        /// </summary>
        [Size(256)]
        public string SourceTitle { get; set; }

        /// <summary>
        /// 来源地址。
        /// </summary>
        [Size(256)]
        public string SourceUrl { get; set; }

        /// <summary>
        /// 作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 作者ID。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int AuthorId { get; set; }

        /// <summary>
        /// 收藏数量。
        /// </summary>
        [Ignore(Ignore.Update)]
        public int Favorites { get; set; }

        /// <summary>
        /// 分类Id。
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 是否已经索引。
        /// </summary>
        public bool IsSearchIndexed { get; set; }
    }
}