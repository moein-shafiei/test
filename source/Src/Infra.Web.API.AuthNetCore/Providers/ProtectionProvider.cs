using DotFramework.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotFramework.Infra.Web.API.Auth.Providers
{
    public interface IProtectionProvider
    {
        string Protect(AuthenticationTicket ticket);
        AuthenticationTicket UnProtect(string token);
        void Revoke(string token);
    }

    public abstract class ProtectionProvider<TProtectionProvider> : SingletonProvider<TProtectionProvider>, IProtectionProvider
        where TProtectionProvider : class, IProtectionProvider
    {
        protected DataProtector _DataProtector;
        protected bool _CompressTicket;

        public virtual void Initialize(IDataProtectionProvider dataProtectionProvider, bool compressTicket, string ticketRepositoryPath)
        {
            _CompressTicket = compressTicket;

            _DataProtector =
                new DataProtector(
                    dataProtectionProvider,
                    _CompressTicket,
                    ticketRepositoryPath);
        }

        public virtual string Protect(AuthenticationTicket ticket)
        {
            return _DataProtector.Protect(ticket);
        }

        public virtual AuthenticationTicket UnProtect(string token)
        {
            return _DataProtector.UnProtect(token);
        }

        public virtual void Revoke(string token)
        {
            if (_CompressTicket)
            {
                _DataProtector.Revoke(token);
            }
            else
            {
                throw new NotSupportedException("Uncompressed token cannot be revoked!");
            }
        }
    }

    public class AuthenticationProtectionProvider : ProtectionProvider<AuthenticationProtectionProvider>
    {
        private AuthenticationProtectionProvider()
        {

        }
    }
}
