using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class AuthorizeStartedEventArgs : EventArgs
    {
        public readonly AuthorizeRequest Request;

        public AuthorizeStartedEventArgs(AuthorizeRequest request)
        {
            Request = request;
        }
    }
}