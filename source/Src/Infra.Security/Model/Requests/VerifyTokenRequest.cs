using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class VerifyTokenRequest
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
