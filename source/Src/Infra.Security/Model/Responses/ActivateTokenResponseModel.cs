using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class ActivateTokenResponseModel : TokenResponseModel
    {
        [JsonProperty("resetPasswordToken")]
        public string ResetPasswordToken { get; set; }
    }
}
