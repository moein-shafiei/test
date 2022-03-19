using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class AuthorizeRequest
    {
        [Required]
        public string ControllerName { get; set; }

        [Required]
        public string ActionName { get; set; }
    }
}
