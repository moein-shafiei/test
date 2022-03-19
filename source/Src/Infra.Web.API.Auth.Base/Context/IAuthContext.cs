using System.Collections.Generic;
using System.Security.Claims;
using DotFramework.Infra.Security.Model;
#if NETFRAMEWORK
using System.Net.Http; 
#else
using Microsoft.AspNetCore.Http;
#endif

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public interface IAuthContext
    {
        string AccessToken { get; }

        List<string> Roles { get; }

        string UserType { get; }

        ClaimsPrincipal User { get; }

        string UserName { get; }

        string Initiator { get; }

        UserDataResponseModel UserData { get; set; }

        ClientDataResponseModel ClientData { get; set; }

#if NETFRAMEWORK
        HttpRequestMessage Request { get; }
#else
        HttpContext Request { get; }
#endif
    }
}