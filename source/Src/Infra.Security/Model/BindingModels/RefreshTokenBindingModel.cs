using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class RefreshTokenBindingModel
    {
        [Required]
        [Display(Name = "Refresh Token")]
        public string RefreshToken { get; set; }
    }
}
