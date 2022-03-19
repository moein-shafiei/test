using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class GrantActivationRequest : AuthenticationRequestBase
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
