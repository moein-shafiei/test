using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Web.API.Auth.Base.Providers
{
    public class BaseTicket
    {
        public string ProtectedToken { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}
