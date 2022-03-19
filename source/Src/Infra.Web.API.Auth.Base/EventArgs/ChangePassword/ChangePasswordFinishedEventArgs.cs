using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ChangePasswordFinishedEventArgs : EventArgs
    {
        public readonly ChangePasswordRequest Request;
        public readonly ChangePasswordResponse Response;

        public ChangePasswordFinishedEventArgs(ChangePasswordRequest request, ChangePasswordResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}