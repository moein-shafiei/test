using DotFramework.Infra.Security.Model;
using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GenerateClientEncryptionTokenStartedEventArgs : EventArgs
    {
        public readonly GenerateClientEncryptionTokenRequest Request;

        public GenerateClientEncryptionTokenStartedEventArgs(GenerateClientEncryptionTokenRequest request)
        {
            Request = request;
        }
    }
}