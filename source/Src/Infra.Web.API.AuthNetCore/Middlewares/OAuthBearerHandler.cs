using DotFramework.Infra.Web.API.Auth.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotFramework.Infra.Web.API.Auth
{
    internal class OAuthBearerHandler
    {
        public AuthenticationTicket Authenticate(HttpContext context)
        {
            var token = GetToken(context);
            AuthenticationTicket ticket = AuthenticationProtectionProvider.Instance.UnProtect(token);

            return ticket;
        }

        public string GetToken(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                try
                {
                    var authorizeHeader = context.Request.Headers["Authorization"];
                    if (authorizeHeader != StringValues.Empty)
                    {
                        var authType = "Bearer";
                        string token = authorizeHeader.FirstOrDefault();

                        if (token?.StartsWith(authType, StringComparison.OrdinalIgnoreCase) ?? false)
                        {
                            var regEx = new Regex(authType, RegexOptions.IgnoreCase);
                            token = regEx.Replace(token, string.Empty).TrimStart();

                            return token;
                        }

                        authType = "Basic";

                        if (token?.StartsWith(authType, StringComparison.OrdinalIgnoreCase) ?? false)
                        {
                            var regEx = new Regex(authType, RegexOptions.IgnoreCase);
                            token = regEx.Replace(token, string.Empty).TrimStart();

                            return token;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return "";
        }
    }
}
