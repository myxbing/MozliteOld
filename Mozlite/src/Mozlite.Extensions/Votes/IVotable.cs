namespace Mozlite.Extensions.Votes
{
    /// <summary>
    /// 可投票接口。
    /// </summary>
    public interface IVotable
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 投票得分。
        /// </summary>
        double VScore { get; set; }

        /// <summary>
        /// 投票人数。
        /// </summary>
        int Voters { get; set; }
    }
}