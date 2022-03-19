using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class RefreshTokenFinishedEventArgs : EventArgs
    {
        public readonly RefreshTokenRequest Request;
        public readonly ObtainLocalAccessTokenResponse Response;

        public RefreshTokenFinishedEventArgs(RefreshTokenRequest request, ObtainLocalAccessTokenResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}