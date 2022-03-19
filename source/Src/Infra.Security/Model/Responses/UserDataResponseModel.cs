using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class UserDataResponseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public List<String> Roles { get; set; }
    }
}
