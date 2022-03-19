using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External Access Token")]
        public string ExternalAccessToken { get; set; }
    }
}