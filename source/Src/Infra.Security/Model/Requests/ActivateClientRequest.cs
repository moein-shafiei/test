using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class ActivateClientRequest
    {
        [Required]
        public string TemporaryClientID { get; set; }
        public string TemporaryClientSecret { get; set; }
        public string DeviceIdentifier { get; set; }
    }
}
