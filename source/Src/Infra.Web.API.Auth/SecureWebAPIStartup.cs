using DotFramework.Core.Configuration;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using DotFramework.Infra.Web.API.Auth.Base;
using Microsoft.Owin.Security.OAuth;
using Owin; 
using System;
using DotFramework.Infra.Web.API.Auth.Providers;

namespace DotFramework.Infra.Web.API.Auth
{
    public class SecureWebAPIStartup : WebAPIStartup
    {
        public static TimeSpan AccessTokenExpireTimeSpan { get; protected set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; protected set; }

        public static IAuthenticationService AuthenticationService { get; protected set; }

        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            TokenProviderFactory.Instance.Configure(new TokenProvider(AuthenticationProtectionProvider.Instance, SecureWebAPIStartup.AccessTokenExpireTimeSpan));
            AuthContextProvider.Configure(new AuthContext());

            AccessTokenExpireTimeSpan = AppSettingsManager.Instance.Get<TimeSpan>("AccessTokenExpireTimeSpan", new TimeSpan(0, 10, 0));

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}