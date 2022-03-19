using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class LoginFinishedEventArgs : EventArgs
    {
        public readonly LoginRequest Request;
        public readonly ObtainLocalAccessTokenResponse Response;

        public LoginFinishedEventArgs(LoginRequest request, ObtainLocalAccessTokenResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}