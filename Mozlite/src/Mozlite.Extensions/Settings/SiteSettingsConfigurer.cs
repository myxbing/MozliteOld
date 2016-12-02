using Microsoft.Extensions.DependencyInjection;
using Mozlite.Core;

namespace Mozlite.Extensions.Settings
{
    /// <summary>
    /// 网站服务配置。
    /// </summary>
    public class SiteSettingsConfigurer: IServiceConfigurer
    {
        /// <summary>
        /// 配置服务方法。
        /// </summary>
        /// <param name="services">服务集合实例。</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(service => service.GetRequiredService<ISettingsManager>().GetSettings<SiteSettings>());
        }
    }
}