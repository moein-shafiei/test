using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GenerateClientEncryptionTokenFinishedEventArgs : EventArgs
    {
        public readonly GenerateClientEncryptionTokenRequest Request;
        public readonly GenerateClientEncryptionTokenResponse Response;

        public GenerateClientEncryptionTokenFinishedEventArgs(GenerateClientEncryptionTokenRequest request, GenerateClientEncryptionTokenResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}