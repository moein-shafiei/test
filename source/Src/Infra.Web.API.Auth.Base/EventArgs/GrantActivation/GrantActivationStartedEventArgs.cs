using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GrantActivationStartedEventArgs : EventArgs
    {
        public readonly GrantActivationRequest Request;

        public GrantActivationStartedEventArgs(GrantActivationRequest request)
        {
            Request = request;
        }
    }
}