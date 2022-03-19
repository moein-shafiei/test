using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DotFramework.Infra.Security.Model
{
    public class UpdateUserRequest
    {
        [Required]
        public string UserName { get; set; }

        public String FullName { get; set; }

        public String Email { get; set; }

        public String AccountPolicyName { get; set; }
    }
}
