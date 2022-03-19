using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ResetPasswordFinishedEventArgs : EventArgs
    {
        public readonly ResetPasswordRequest Request;
        public readonly ResetPasswordResponse Response;

        public ResetPasswordFinishedEventArgs(ResetPasswordRequest request, ResetPasswordResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}