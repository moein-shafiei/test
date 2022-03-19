using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class ForgetPasswordRequest : AuthenticationRequestBase
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "RedirectUrl")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// send data as a list of key values. ex: TemplateKeyValues: [{"Key": "MaxItems", "Value": "3.0"} ]
        /// </summary>
        [Display(Name = "TemplateKeyValues")]
        public string TemplateKeyValues { get; set; }
    }
}
