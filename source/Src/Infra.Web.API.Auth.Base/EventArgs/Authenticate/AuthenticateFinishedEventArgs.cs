using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class AuthenticateFinishedEventArgs : EventArgs
    {
        public readonly AuthenticateRequest Request;
        public readonly AuthenticateResponseModel Response;

        public AuthenticateFinishedEventArgs(AuthenticateRequest request, AuthenticateResponseModel response)
        {
            Request = request;
            Response = response;
        }
    }
}