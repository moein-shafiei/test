using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GetUserDataFinishedEventArgs : EventArgs
    {
        public readonly UserDataResponseModel Response;

        public GetUserDataFinishedEventArgs(UserDataResponseModel response)
        {
            Response = response;
        }
    }
}