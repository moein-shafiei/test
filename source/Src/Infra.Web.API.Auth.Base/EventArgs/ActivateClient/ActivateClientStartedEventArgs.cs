using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ActivateClientStartedEventArgs : EventArgs
    {
        public readonly ActivateClientRequest Request;

        public ActivateClientStartedEventArgs(ActivateClientRequest request)
        {
            Request = request;
        }
    }
}