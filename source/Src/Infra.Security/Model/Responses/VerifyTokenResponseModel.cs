using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class VerifyTokenResponseModel : IUserIdentity
    {
        public string UserName { get; set; }

        public string GroupToken { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime IssuedAt { get; set; }

        public int ExpiresIn { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string ExternalUserID { get; set; }

        public string ProfilePicture { get; set; }

        public List<String> Friends { get; set; }

        public List<String> Roles { get; set; }

        public bool FirstTimeLogin { get; set; }

        public Dictionary<String, String> Properties { get; set; }
    }
}
