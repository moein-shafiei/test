using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ObtainLocalAccessTokenFinishedEventArgs : EventArgs
    {
        public readonly ObtainLocalAccessTokenRequest Request;
        public readonly ObtainLocalAccessTokenResponse Response;

        public ObtainLocalAccessTokenFinishedEventArgs(ObtainLocalAccessTokenRequest request, ObtainLocalAccessTokenResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}