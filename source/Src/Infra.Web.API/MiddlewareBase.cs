using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DotFramework.Infra.Web.API
{
    public abstract class MiddlewareBase
    {
        private readonly RequestDelegate _next;

        public MiddlewareBase(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Request.Path != "/error")
            {
                ProcessRequest(context);
            }

            await _next.Invoke(context);
        }

        public abstract void ProcessRequest(HttpContext context);
    }
}
