using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security
{
    public interface IUserIdentity
    {
        string UserName { get; set; }

        string GroupToken { get; set; }

        string Email { get; set; }

        string FullName { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        DateTime IssuedAt { get; set; }

        int ExpiresIn { get; set; }

        DateTime ExpiresAt { get; set; }

        string ExternalUserID { get; set; }

        string ProfilePicture { get; set; }

        List<String> Friends { get; set; }

        List<String> Roles { get; set; }

        bool FirstTimeLogin { get; set; }

        Dictionary<String, String> Properties { get; set; }
    }
}
