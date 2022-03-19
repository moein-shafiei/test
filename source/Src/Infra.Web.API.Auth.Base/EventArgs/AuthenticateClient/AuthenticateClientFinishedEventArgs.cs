using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class AuthenticateClientFinishedEventArgs : EventArgs
    {
        public readonly AuthenticateClientRequest Request;
        public readonly AuthenticateClientResponse Response;

        public AuthenticateClientFinishedEventArgs(AuthenticateClientRequest request, AuthenticateClientResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}