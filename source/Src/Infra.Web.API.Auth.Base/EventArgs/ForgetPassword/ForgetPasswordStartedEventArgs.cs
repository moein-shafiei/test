using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ForgetPasswordStartedEventArgs : EventArgs
    {
        public readonly ForgetPasswordRequest Request;

        public ForgetPasswordStartedEventArgs(ForgetPasswordRequest request)
        {
            Request = request;
        }
    }
}