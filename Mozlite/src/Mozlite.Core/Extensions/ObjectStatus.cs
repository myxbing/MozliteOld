namespace Mozlite.Extensions
{
    /// <summary>
    /// 对象状态。
    /// </summary>
    public enum ObjectStatus
    {
        /// <summary>
        /// 禁用。
        /// </summary>
        Disabled = -1,

        /// <summary>
        /// 正常。
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 采集的数据。
        /// </summary>
        Spider,

        /// <summary>
        /// 等待验证。
        /// </summary>
        PaddingApproved,
    }
}