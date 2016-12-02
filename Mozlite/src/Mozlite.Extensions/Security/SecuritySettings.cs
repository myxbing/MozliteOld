namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 用户配置。
    /// </summary>
    public class SecuritySettings
    {
        /// <summary>
        /// 媒体类型。
        /// </summary>
        public const string MediaType = "users";

        /// <summary>
        /// 默认头像。
        /// </summary>
        public string DefaultAvatar { get; set; }
    }
}