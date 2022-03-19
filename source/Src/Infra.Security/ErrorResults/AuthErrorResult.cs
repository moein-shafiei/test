using DotFramework.Core.Web;
using Newtonsoft.Json;

namespace DotFramework.Infra.Security
{
    public class AuthErrorResult : ErrorResult
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
