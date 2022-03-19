using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class LoginRequest : AuthenticationRequestBase
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
