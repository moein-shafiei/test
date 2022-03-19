using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class ReauthenticateRequest : AuthenticationRequestBase
    {
        //[Required]
        public string RefreshToken { get; set; }
    }
}
