using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class ActivateLocalAccessTokenResponse : ObtainLocalAccessTokenResponse
    {
        public string ResetPasswordToken { get; set; }
    }
}
