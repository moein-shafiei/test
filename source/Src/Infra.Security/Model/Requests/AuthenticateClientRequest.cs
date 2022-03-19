using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Security.Model
{
    public class AuthenticateClientRequest
    {
        public string ClientID { get; set; }

        public string ClientSecret { get; set; }

        public string DeviceIdentifier { get; set; }
    }
}
