using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class FirstTimeActivateRequest
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}
