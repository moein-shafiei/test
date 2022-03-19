using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ForgetPasswordFinishedEventArgs : EventArgs
    {
        public readonly ForgetPasswordRequest Request;
        public readonly ForgetPasswordResponse Response;

        public ForgetPasswordFinishedEventArgs(ForgetPasswordRequest request, ForgetPasswordResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}