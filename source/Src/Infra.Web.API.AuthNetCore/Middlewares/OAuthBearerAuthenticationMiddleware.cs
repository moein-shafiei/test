using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotFramework.Core.Configuration;
using DotFramework.Infra.Web.API.Auth.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Primitives;

namespace DotFramework.Infra.Web.API.Auth.Middleware
{
    public class OAuthBearerAuthenticationMiddleware
    {
        private IDataProtectionProvider _protectionProvider;
        private readonly RequestDelegate _next;

        public OAuthBearerAuthenticationMiddleware(RequestDelegate next, IDataProtectionProvider protectionProvider)
        {
            _next = next;
            _protectionProvider = protectionProvider;

            AuthenticationProtectionProvider.Instance
                .Initialize(
                    _protectionProvider,
                    true,
                    AppSettingsManager.Instance.Get("IdentityRepositoryPath", Path.Combine(Directory.GetCurrentDirectory(), "Identity")));
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint != null)
            {
                var ticket = new OAuthBearerHandler().Authenticate(context);

                if (ticket != null)
                {
                    context.User = ticket.Principal;
                }
            }

            await _next(context);
        }
    }
}
