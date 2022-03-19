using DotFramework.Core;
using DotFramework.Core.Configuration;
using DotFramework.Infra.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
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
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly List<string> _purpose = new List<string>() { "TokenMiddleware", "Access_Token", "v2" };
        private readonly string _TicketRepositoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Identity");

        private readonly bool _CompressTicket;

        public DataProtector(IDataProtectionProvider dataProtectionProvider, bool compressTicket, string ticketRepositoryPath)
        {
            if (_dataProtectionProvider == null)
            {
                _dataProtectionProvider = dataProtectionProvider.CreateProtector(_purpose);
            }

            _CompressTicket = compressTicket;
            _TicketRepositoryPath = ticketRepositoryPath;

            if (_CompressTicket)
            {
                if (String.IsNullOrEmpty(_TicketRepositoryPath))
                {
                    throw new ArgumentNullException("TicketRepositoryPath");
                }
                else
                {
                    if (!Directory.Exists(_TicketRepositoryPath))
                    {
                        Directory.CreateDirectory(_TicketRepositoryPath);
                    }
                }
            }
        }

        public string Protect(AuthenticationTicket ticket)
        {
            if (_dataProtectionProvider == null)
            {
                throw new Exception("IDataProtectionProvider is not provided through initialization");
            }

            var sticket = TicketSerializer.Default.Serialize(CompressTicket(ticket));
            var protector = _dataProtectionProvider.CreateProtector(_purpose);

            return protector.Protect(Encoding.UTF8.GetString(sticket));
        }

        public AuthenticationTicket UnProtect(string token)
        {
            if (_dataProtectionProvider == null)
            {
                throw new Exception("IDataProtectionProvider is not provided through initialization");
            }

            var protector = _dataProtectionProvider.CreateProtector(_purpose);

            try
            {
                string decryptedToken = protector.Unprotect(token);
                AuthenticationTicket ticket = TicketSerializer.Default.Deserialize(Encoding.UTF8.GetBytes(decryptedToken));

                if (ticket.Properties.ExpiresUtc < DateTime.UtcNow)
                {
                    RemoveIdentity(ticket);
                    throw new UnauthorizedHttpException();
                }

                return ExtractTicket(ticket);
            }
            catch (Exception)
            {
                throw new UnauthorizedHttpException();
            }
        }

        public void Revoke(string token)
        {
            if (_dataProtectionProvider == null)
            {
                throw new Exception("IDataProtectionProvider is not provided through initialization");
            }

            var protector = _dataProtectionProvider.CreateProtector(_purpose);

            try
            {
                string decryptedToken = protector.Unprotect(token);
                AuthenticationTicket ticket = TicketSerializer.Default.Deserialize(Encoding.UTF8.GetBytes(decryptedToken));

                RemoveIdentity(ticket);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private AuthenticationTicket CompressTicket(AuthenticationTicket ticket)
        {
            if (!_CompressTicket)
            {
                return ticket;
            }
            else
            {
                var groupTokenClaim = ticket.Principal.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.GroupToken) ?? new Claim(CustomClaimTypes.GroupToken, CreateShortGuid());
                var tokenIdentifier = CreateShortGuid();

                var tokenIdentifierClaim = new Claim(CustomClaimTypes.TokenIdentifier, tokenIdentifier);

                ClaimsIdentity oAuthIdentity_compressed = new ClaimsIdentity(new List<Claim> { groupTokenClaim, tokenIdentifierClaim }, ticket.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal_compressed = new ClaimsPrincipal(oAuthIdentity_compressed);
                AuthenticationTicket ticket_compressed = new AuthenticationTicket(claimsPrincipal_compressed, ticket.Properties, ticket.AuthenticationScheme);

                SaveIdentityClaims(ticket, groupTokenClaim.Value, tokenIdentifier);

                return ticket_compressed;
            }
        }

        private AuthenticationTicket ExtractTicket(AuthenticationTicket ticket)
        {
            if (!_CompressTicket)
            {
                return ticket;
            }
            else
            {
                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(RetrieveIdentityClaims(ticket), ticket.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(oAuthIdentity);

                AuthenticationTicket new_ticket = new AuthenticationTicket(claimsPrincipal, ticket.Properties, ticket.AuthenticationScheme);

                return new_ticket;
            }
        }

        private void RemoveIdentity(AuthenticationTicket ticket)
        {
            if (ticket.Principal.Claims.Any(c => c.Type == CustomClaimTypes.GroupToken))
            {
                var groupTokenClaim = ticket.Principal.Claims.First(c => c.Type == CustomClaimTypes.GroupToken);

                if (!String.IsNullOrEmpty(groupTokenClaim.Value))
                {
                    string groupIdentityPath = Path.Combine(_TicketRepositoryPath, groupTokenClaim.Value);

                    if (Directory.Exists(groupIdentityPath))
                    {
                        Directory.Delete(groupIdentityPath, true);
                    }
                }
            }
        }

        private void SaveIdentityClaims(AuthenticationTicket ticket, string groupHandler, string fileHandle)
        {
            var sticket = CustomTicketSerializer.Default.Serialize(ticket);

            var protector = _dataProtectionProvider.CreateProtector(_purpose);
            var protectedTicket = protector.Protect(Encoding.UTF8.GetString(sticket));

            string identityFileDirPath = Path.Combine(_TicketRepositoryPath, groupHandler);
            var identityFilePath = Path.Combine(identityFileDirPath, $"{fileHandle}.dat");

            Directory.CreateDirectory(identityFileDirPath);
            File.WriteAllText(identityFilePath, protectedTicket);
        }

        private IEnumerable<Claim> RetrieveIdentityClaims(AuthenticationTicket ticket)
        {
            if (!ticket.Principal.Claims.Any(c => c.Type == CustomClaimTypes.GroupToken))
            {
                throw new NullReferenceException("Group Token Claim cannot be empty.");
            }
            else if (!ticket.Principal.Claims.Any(c => c.Type == CustomClaimTypes.TokenIdentifier))
            {
                throw new NullReferenceException("Token Identifier Claim cannot be empty.");
            }
            else
            {
                var groupTokenClaim = ticket.Principal.Claims.First(c => c.Type == CustomClaimTypes.GroupToken);
                var tokenIdentifierClaim = ticket.Principal.Claims.First(c => c.Type == CustomClaimTypes.TokenIdentifier);

                var identityFilePath = Path.Combine(_TicketRepositoryPath, groupTokenClaim.Value, $"{tokenIdentifierClaim.Value}.dat");
                string token = File.ReadAllText(identityFilePath);

                var protector = _dataProtectionProvider.CreateProtector(_purpose);
                string decryptedToken = protector.Unprotect(token);
                AuthenticationTicket new_ticket = CustomTicketSerializer.Default.Deserialize(Encoding.UTF8.GetBytes(decryptedToken));

                return new_ticket.Principal.Claims;
            }
        }

        private string CreateShortGuid()
        {
            var ticks = new DateTime(2016, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            return ans.ToString("x");
        }
    }
}
