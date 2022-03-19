using DotFramework.Core;
using Microsoft.Owin.Security;

namespace DotFramework.Infra.Web.API.Auth.Providers
{
    public interface IProtectionProvider
    {
        string Protect(AuthenticationTicket ticket);
        AuthenticationTicket UnProtect(string token);
    }

    public abstract class ProtectionProvider<TProtectionProvider> : SingletonProvider<TProtectionProvider>, IProtectionProvider
        where TProtectionProvider : class, IProtectionProvider
    {
        protected readonly DataProtector _DataProtector = new DataProtector();

        public ProtectionProvider()
        {
            _DataProtector = new DataProtector();
        }

        public virtual string Protect(AuthenticationTicket ticket)
        {
            return _DataProtector.Protect(ticket);
        }

        public virtual AuthenticationTicket UnProtect(string token)
        {
            return _DataProtector.UnProtect(token);
        }
    }

    public class AuthenticationProtectionProvider : ProtectionProvider<AuthenticationProtectionProvider>
    {
        private AuthenticationProtectionProvider()
        {

        }
    }
}
