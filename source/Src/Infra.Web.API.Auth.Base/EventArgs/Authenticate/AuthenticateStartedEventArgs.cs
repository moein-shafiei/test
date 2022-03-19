using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class AuthenticateStartedEventArgs : EventArgs
    {
        public readonly AuthenticateRequest Request;

        public AuthenticateStartedEventArgs(AuthenticateRequest request)
        {
            Request = request;
        }
    }
}