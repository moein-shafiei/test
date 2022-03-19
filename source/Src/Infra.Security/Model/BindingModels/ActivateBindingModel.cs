using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Security.Model
{
    public class ActivateBindingModel
    {
        [Display(Name = "UserName")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get; set; }
    }
}
