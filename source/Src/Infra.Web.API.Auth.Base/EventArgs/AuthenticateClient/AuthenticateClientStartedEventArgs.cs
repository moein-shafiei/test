using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class AuthenticateClientStartedEventArgs : EventArgs
    {
        public readonly AuthenticateClientRequest Request;

        public AuthenticateClientStartedEventArgs(AuthenticateClientRequest request)
        {
            Request = request;
        }
    }
}