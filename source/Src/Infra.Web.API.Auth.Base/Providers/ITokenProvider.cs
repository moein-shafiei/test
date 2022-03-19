using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Web.API.Auth.Base.Providers
{
    public interface ITokenProvider
    {
        BaseTicket GetBaseTicket(List<Claim> claims);
        IEnumerable<Claim> UnProtect(string token);
        void Revoke(string token);
    }
}
