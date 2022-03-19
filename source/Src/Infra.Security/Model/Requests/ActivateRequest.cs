using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class ActivateRequest : AuthenticationRequestBase
    {
        [Required]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }
    }
}
