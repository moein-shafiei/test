using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Security.Model
{
    public class AuthenticationRequestBase
    {
        public string Initiator { get; set; }

        public Dictionary<String, String> AdditionalParameters { get; set; }
    }
}
