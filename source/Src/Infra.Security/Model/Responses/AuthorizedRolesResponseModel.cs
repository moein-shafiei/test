using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class AuthorizedRolesResponseModel
    {
        [JsonProperty("Roles")]
        public List<String> Roles { get; set; }

        [JsonProperty("Error")]
        public String Error { get; set; }

        public bool HasError
        {
            get
            {
                return !String.IsNullOrEmpty(Error);
            }
        }

        [JsonProperty("NeedAuthorization")]
        public bool NeedAuthorization { get; set; }
    }
}
