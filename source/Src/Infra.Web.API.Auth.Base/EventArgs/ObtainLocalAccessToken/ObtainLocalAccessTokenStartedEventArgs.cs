using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ObtainLocalAccessTokenStartedEventArgs : EventArgs
    {
        public readonly ObtainLocalAccessTokenRequest Request;

        public ObtainLocalAccessTokenStartedEventArgs(ObtainLocalAccessTokenRequest request)
        {
            Request = request;
        }
    }
}