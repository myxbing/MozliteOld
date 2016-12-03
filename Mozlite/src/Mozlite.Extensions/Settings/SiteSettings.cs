namespace Mozlite.Extensions.Settings
{
    /// <summary>
    /// 网站配置。
    /// </summary>
    public class SiteSettings
    {
        /// <summary>
        /// 网站名称。
        /// </summary>
        public string SiteName { get; set; } = "MOZLITE";

        /// <summary>
        /// 版权信息。
        /// </summary>
        public string Copyright { get; set; } = "© 2006-2016 www.mozlite.com. All rights reserved.";

        /// <summary>
        /// 标题代码。
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 尾部代码。
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// 首页标题代码。
        /// </summary>
        public string IndexHeader { get; set; }

        /// <summary>
        /// 首页尾部代码。
        /// </summary>
        public string IndexFooter { get; set; }
    }
}