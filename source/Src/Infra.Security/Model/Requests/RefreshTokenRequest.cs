using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class RefreshTokenRequest : AuthenticationRequestBase
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
