using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class RefreshTokenStartedEventArgs : EventArgs
    {
        public readonly RefreshTokenRequest Request;

        public RefreshTokenStartedEventArgs(RefreshTokenRequest request)
        {
            Request = request;
        }
    }
}