using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mozlite.Core;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 应用程序启动配置类。
    /// </summary>
    public class AuthenticationConfigurer : IApplicationConfigurer
    {
        /// <summary>
        /// 配置中间件使用模型。
        /// </summary>
        /// <param name="app">应用程序构建接口。</param>
        /// <param name="configuration">配置实例。</param>
        public void Configure(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseIdentity()
               .UseCookieAuthentication(new CookieAuthenticationOptions
               {
                   AutomaticAuthenticate = true,
                   AutomaticChallenge = true,
                   CookieName = "MozliteAspNetCore",
                   LoginPath = new PathString("/login"),
                   LogoutPath = new PathString("/logout"),
               });
            //app.UseFacebookAuthentication(new FacebookOptions
            //    {
            //        AppId = "901611409868059",
            //        AppSecret = "4aa3c530297b1dcebc8860334b39668b"
            //    })
            //    .UseGoogleAuthentication(new GoogleOptions
            //    {
            //        ClientId = "514485782433-fr3ml6sq0imvhi8a7qir0nb46oumtgn9.apps.googleusercontent.com",
            //        ClientSecret = "V2nDD9SkFbvLTqAUBWBBxYAL"
            //    })
            //    .UseTwitterAuthentication(new TwitterOptions
            //    {
            //        ConsumerKey = "BSdJJ0CrDuvEhpkchnukXZBUv",
            //        ConsumerSecret = "xKUNuKhsRdHD03eLn67xhPAyE1wFFEndFo1X2UJaK2m1jdAxf4"
            //    });
        }
    }
}