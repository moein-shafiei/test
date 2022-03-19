using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class AuthenticateRequest : AuthenticationRequestBase
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
