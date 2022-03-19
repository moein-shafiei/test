using DotFramework.Core;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DotFramework.Infra.Web.API.Auth.Providers
{
    public class DataProtector
    {
        public string Protect(AuthenticationTicket ticket)
        {
            return SecureWebAPIStartup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
        }

        public AuthenticationTicket UnProtect(string token)
        {
            return SecureWebAPIStartup.OAuthBearerOptions.AccessTokenFormat.Unprotect(token);
        }
    }
}
