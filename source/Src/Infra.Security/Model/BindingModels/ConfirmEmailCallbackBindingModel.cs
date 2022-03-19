using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Security.Model
{
    public class ConfirmEmailCallbackBindingModel
    {
        [Display(Name = "Email")]
        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Token")]
        [JsonProperty("token")]
        [Required]
        public string Token { get; set; }

        [Display(Name = "RedirectUrl")]
        [JsonProperty("redirectUrl")]
        [Required]
        public string RedirectUrl { get; set; }
    }
}
