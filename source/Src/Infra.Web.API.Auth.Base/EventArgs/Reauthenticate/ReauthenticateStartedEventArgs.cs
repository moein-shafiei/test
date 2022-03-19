using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ReauthenticateStartedEventArgs : EventArgs
    {
        public readonly ReauthenticateRequest Request;

        public ReauthenticateStartedEventArgs(ReauthenticateRequest request)
        {
            Request = request;
        }
    }
}