using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class ObtainLocalAccessTokenRequest : AuthenticationRequestBase
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string Provider { get; set; }
    }
}
