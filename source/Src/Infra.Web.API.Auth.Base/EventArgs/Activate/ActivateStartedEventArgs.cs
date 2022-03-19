using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ActivateStartedEventArgs : EventArgs
    {
        public readonly ActivateRequest Request;

        public ActivateStartedEventArgs(ActivateRequest request)
        {
            Request = request;
        }
    }
}