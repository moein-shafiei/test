using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GrantActivationFinishedEventArgs : EventArgs
    {
        public readonly GrantActivationRequest Request;
        public readonly ActivateLocalAccessTokenResponse Response;

        public GrantActivationFinishedEventArgs(GrantActivationRequest request, ActivateLocalAccessTokenResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}