using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ReauthenticateFinishedEventArgs : EventArgs
    {
        public readonly ReauthenticateRequest Request;
        public readonly ObtainLocalAccessTokenResponse Response;

        public ReauthenticateFinishedEventArgs(ReauthenticateRequest request, ObtainLocalAccessTokenResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}