using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class VerifyClientTokenFinishedEventArgs : EventArgs
    {
        public readonly VerifyTokenRequest Request;
        public readonly VerifyClientTokenResponseModel Response;

        public VerifyClientTokenFinishedEventArgs(VerifyTokenRequest request, VerifyClientTokenResponseModel response)
        {
            Request = request;
            Response = response;
        }
    }
}