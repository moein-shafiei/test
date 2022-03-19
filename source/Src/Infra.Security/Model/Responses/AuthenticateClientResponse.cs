using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class AuthenticateClientResponse : IClientIdentity
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("client_identifier")]
        public string ClientIdentifier { get; set; }

        [JsonProperty("client_name")]
        public string ClientName { get; set; }

        [JsonProperty(".issued")]
        public DateTime IssuedAt { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(".expires")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("properties")]
        public Dictionary<String, String> Properties { get; set; }
    }
}
