using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GetClientDataFinishedEventArgs : EventArgs
    {
        public readonly ClientDataResponseModel Response;

        public GetClientDataFinishedEventArgs(ClientDataResponseModel response)
        {
            Response = response;
        }
    }
}