using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ValidateTokenFinishedEventArgs : EventArgs
    {
        public readonly AuthenticateResponseModel Response;

        public ValidateTokenFinishedEventArgs(AuthenticateResponseModel response)
        {
            Response = response;
        }
    }
}