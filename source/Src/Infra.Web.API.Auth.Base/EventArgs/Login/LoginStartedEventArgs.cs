using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class LoginStartedEventArgs : EventArgs
    {
        public readonly LoginRequest Request;

        public LoginStartedEventArgs(LoginRequest request)
        {
            Request = request;
        }
    }
}