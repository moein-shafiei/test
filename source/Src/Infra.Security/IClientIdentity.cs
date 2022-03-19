using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security
{
    public interface IClientIdentity
    {
        string ClientIdentifier { get; set; }

        string ClientName { get; set; }

        DateTime IssuedAt { get; set; }

        int ExpiresIn { get; set; }

        DateTime ExpiresAt { get; set; }

        Dictionary<String, String> Properties { get; set; }
    }
}
