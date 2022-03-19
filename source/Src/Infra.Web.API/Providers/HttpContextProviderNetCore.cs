using Microsoft.AspNetCore.Http;

namespace DotFramework.Infra.Web.API
{
    public class HttpContextProvider
    {
        private static IHttpContextAccessor _HttpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current
        {
            get
            {
                return _HttpContextAccessor.HttpContext;
            }
        }
    }
}
