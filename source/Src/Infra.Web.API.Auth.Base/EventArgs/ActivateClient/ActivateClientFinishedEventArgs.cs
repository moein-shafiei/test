using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ActivateClientFinishedEventArgs : EventArgs
    {
        public readonly ActivateClientRequest Request;
        public readonly ActivateClientResponse Response;

        public ActivateClientFinishedEventArgs(ActivateClientRequest request, ActivateClientResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}