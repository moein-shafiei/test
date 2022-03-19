using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class RegisterFinishedEventArgs : EventArgs
    {
        public readonly RegisterRequest Request;
        public readonly ObtainLocalAccessTokenResponse Response;

        public RegisterFinishedEventArgs(RegisterRequest request, ObtainLocalAccessTokenResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}