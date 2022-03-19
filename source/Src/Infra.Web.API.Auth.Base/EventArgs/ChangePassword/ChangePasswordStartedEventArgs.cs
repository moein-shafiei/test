using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ChangePasswordStartedEventArgs : EventArgs
    {
        public readonly ChangePasswordRequest Request;

        public ChangePasswordStartedEventArgs(ChangePasswordRequest request)
        {
            Request = request;
        }
    }
}