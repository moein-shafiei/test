using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class VerifyClientTokenStartedEventArgs : EventArgs
    {
        public readonly VerifyTokenRequest Request;

        public VerifyClientTokenStartedEventArgs(VerifyTokenRequest request)
        {
            Request = request;
        }
    }
}