using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Application Code")]
        public string ApplicationCode { get; set; }

        [Required]
        [Display(Name = "Provider")]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "External Access Token")]
        public string ExternalAccessToken { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "External Identity")]
        public string ExternalIdentity { get; set; }

        [Display(Name = "Initial Role")]
        public string InitialRole { get; set; }
    }
}