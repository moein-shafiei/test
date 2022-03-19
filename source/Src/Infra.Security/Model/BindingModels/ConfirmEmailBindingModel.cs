using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Security.Model
{
    public class ConfirmEmailBindingModel
    {
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "RedirectUrl")]
        [Required]
        public string RedirectUrl { get; set; }
    }
}
