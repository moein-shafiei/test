using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class UserInquiryRequest
    {
        [Required]
        public string UserName { get; set; }
    }
}
