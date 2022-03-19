using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class AuthorizationBindingModel
    {
        [Required]
        [Display(Name = "Access Token")]
        public string AccessToken { get; set; }
    }
}
