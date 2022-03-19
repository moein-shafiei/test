using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Security.Model
{
    public class GrantActivationBindingModel : AuthenticationRequestBase
    {
        [Display(Name = "UserName")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }
    }
}
