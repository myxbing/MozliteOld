using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Mozlite.Core;
using Mozlite.Extensions.Settings;

namespace Mozlite.Extensions.Security.Services
{
    /// <summary>
    /// 当前用户服务配置。
    /// </summary>
    public class SecurityConfigurer : IServiceConfigurer
    {
        /// <summary>
        /// 配置服务方法。
        /// </summary>
        /// <param name="services">服务集合实例。</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
               .Configure<IdentityOptions>(options =>
               {
                   //密码验证
                   var password = new PasswordOptions();
                   password.RequireDigit = false;
                   password.RequireLowercase = false;
                   password.RequireUppercase = false;
                   password.RequireNonAlphanumeric = false;
                   password.RequiredLength = 6;
                   options.Password = password;
                   //用户配置
                   var user = new UserOptions();
                   user.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
                   user.RequireUniqueEmail = true;
                   options.User = user;
                   //需要激活电子邮件
                   options.SignIn.RequireConfirmedEmail = true;
               })
               .AddScoped(service => service.GetRequiredService<IUserManager>().GetUser())
               .AddScoped(service=>service.GetRequiredService<ISettingsManager>().GetSettings<SecuritySettings>());
        }
    }
}