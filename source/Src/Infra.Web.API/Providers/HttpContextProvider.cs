using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DotFramework.Infra.Web.API
{
    public class HttpContextProvider
    {
        public static HttpContext Current
        {
            get
            {
                return HttpContext.Current;
            }
        }
    }
}
