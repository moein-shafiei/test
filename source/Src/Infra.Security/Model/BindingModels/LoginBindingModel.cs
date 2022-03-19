using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class LoginBindingModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Return Url")]
        public string ReturnUrl { get; set; }
    }
}
