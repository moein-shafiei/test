using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class AuthorizeFinishedEventArgs : EventArgs
    {
        public readonly AuthorizeRequest Request;
        public readonly UserDataResponseModel Response;

        public AuthorizeFinishedEventArgs(AuthorizeRequest request, UserDataResponseModel response)
        {
            Request = request;
            Response = response;
        }
    }
}