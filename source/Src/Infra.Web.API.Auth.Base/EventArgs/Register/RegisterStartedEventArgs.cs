using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class RegisterStartedEventArgs : EventArgs
    {
        public readonly RegisterRequest Request;

        public RegisterStartedEventArgs(RegisterRequest request)
        {
            Request = request;
        }
    }
}