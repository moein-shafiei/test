using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class ObtainLocalAccessTokenBindingModel
    {
        [Required]
        [Display(Name = "Access Token")]
        public string AccessToken { get; set; }

        [Required]
        [Display(Name = "Provider")]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Client ID")]
        public string ClientID { get; set; }
    }
}
