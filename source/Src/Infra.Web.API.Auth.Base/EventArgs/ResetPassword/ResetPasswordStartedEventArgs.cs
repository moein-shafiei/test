using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ResetPasswordStartedEventArgs : EventArgs
    {
        public readonly ResetPasswordRequest Request;

        public ResetPasswordStartedEventArgs(ResetPasswordRequest request)
        {
            Request = request;
        }
    }
}