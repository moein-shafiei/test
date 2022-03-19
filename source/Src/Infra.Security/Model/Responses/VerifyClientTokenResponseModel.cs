using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class VerifyClientTokenResponseModel : IClientIdentity
    {
        public string ClientIdentifier { get; set; }

        public string ClientName { get; set; }

        public DateTime IssuedAt { get; set; }

        public int ExpiresIn { get; set; }

        public DateTime ExpiresAt { get; set; }

        public Dictionary<String, String> Properties { get; set; }
    }
}
