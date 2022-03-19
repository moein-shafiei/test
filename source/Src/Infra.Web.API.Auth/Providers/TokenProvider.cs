using DotFramework.Core;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using DotFramework.Infra.Web.API.Auth;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotFramework.Infra.Web.API.Auth.Providers;

namespace DotFramework.Infra.Web.API.Auth
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IProtectionProvider _ProtectionProvider;
        private readonly TimeSpan _AccessTokenExpireTimeSpan;

        public TokenProvider(IProtectionProvider protectionProvider, TimeSpan accessTokenExpireTimeSpan)
        {
            _ProtectionProvider = protectionProvider;
            _AccessTokenExpireTimeSpan = accessTokenExpireTimeSpan;
        }

        public BaseTicket GetBaseTicket(List<Claim> claims)
        {
            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);

            var properties = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(_AccessTokenExpireTimeSpan),
            };

            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            string protectedToken = _ProtectionProvider.Protect(ticket);

            return new BaseTicket
            {
                ProtectedToken = protectedToken,
                ExpiresAt = ticket.Properties.ExpiresUtc.Value.DateTime,
                IssuedAt = ticket.Properties.IssuedUtc.Value.DateTime,
                ExpiresIn = (int)_AccessTokenExpireTimeSpan.TotalSeconds,
                TokenType = OAuthDefaults.AuthenticationType
            };
        }

        public void Revoke(string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Claim> UnProtect(string token)
        {
            var ticket = _ProtectionProvider.UnProtect(token);
            return ticket.Identity.Claims;
        }
    }
}
