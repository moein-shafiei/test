using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class ObtainLocalAccessTokenResponse : IUserIdentity, IAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("group_token")]
        public string GroupToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime IssuedAt { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        public string ExternalUserID { get; set; }

        public string ProfilePicture { get; set; }

        public List<String> Friends { get; set; }

        public List<String> Roles { get; set; }

        public bool FirstTimeLogin { get; set; }

        public string Initiator { get; set; }

        public Dictionary<String, String> Properties { get; set; }
    }
}
