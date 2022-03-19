using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class ActivateFinishedEventArgs : EventArgs
    {
        public readonly ActivateRequest Request;
        public readonly ActivateResponse Response;

        public ActivateFinishedEventArgs(ActivateRequest request, ActivateResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}