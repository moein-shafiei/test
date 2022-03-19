using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class GenerateClientEncryptionTokenRequest
    {
        [Required]
        public string ClientID { get; set; }
    }
}
