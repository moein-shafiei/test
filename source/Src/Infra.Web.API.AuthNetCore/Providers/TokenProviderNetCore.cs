using DotFramework.Infra.Web.API.Auth.Base.Providers;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DotFramework.Infra.Web.API.Auth.Providers
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

        public virtual BaseTicket GetBaseTicket(List<Claim> claims)
        {
            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims, "Bearer");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(oAuthIdentity);

            var properties = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(_AccessTokenExpireTimeSpan),
            };

            //TODO .net core conversion: authenticationScheme should be dynamic
            AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, properties, "Bearer");

            string protectedToken = _ProtectionProvider.Protect(ticket);

            return new BaseTicket
            {
                ProtectedToken = protectedToken,
                ExpiresAt = ticket.Properties.ExpiresUtc.Value.DateTime,
                IssuedAt = ticket.Properties.IssuedUtc.Value.DateTime,
                ExpiresIn = (int)_AccessTokenExpireTimeSpan.TotalSeconds,
                TokenType = "Bearer"
            };
        }

        public virtual void Revoke(string token)
        {
            _ProtectionProvider.Revoke(token);
        }

        public virtual IEnumerable<Claim> UnProtect(string token)
        {
            var ticket = _ProtectionProvider.UnProtect(token);
            return ticket.Principal.Claims;
        }
    }
}
