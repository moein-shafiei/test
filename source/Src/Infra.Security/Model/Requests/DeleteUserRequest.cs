using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DotFramework.Infra.Security.Model
{
    public class DeleteUserRequest
    {
        [Required]
        public string UserName { get; set; }
    }
}
