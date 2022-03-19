using System;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using DotFramework.Infra.Web.API.Auth.Middleware;
using DotFramework.Infra.Web.API.Auth.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotFramework.Infra.Web.API.Auth
{
    public abstract class SecureWebAPIStartup : WebAPIStartup
    {
        public SecureWebAPIStartup(IConfiguration configuration) : base(configuration)
        {

        }

        public static TimeSpan AccessTokenExpireTimeSpan { get; protected set; }

        public override void InitializeServices(IServiceCollection services)
        {
            base.InitializeServices(services);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }

        public override void InitializeApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.InitializeApplication(app, env);
        }

        protected virtual void InitializeAuthApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            TokenProviderFactory.Instance.Configure(new TokenProvider(AuthenticationProtectionProvider.Instance, SecureWebAPIStartup.AccessTokenExpireTimeSpan));
            AuthContextProvider.Configure(new AuthContext());
            app.UseMiddleware<OAuthBearerAuthenticationMiddleware>();
        }

        protected virtual void InitializeAuthServices(IServiceCollection services)
        {
            services.AddDataProtection();
        }
    }
}
