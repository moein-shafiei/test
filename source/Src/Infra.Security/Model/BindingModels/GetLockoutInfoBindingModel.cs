using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DotFramework.Infra.Security.Model
{
    public class GetLockoutInfoBindingModel : AuthorizationBindingModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
