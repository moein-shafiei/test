using System;
using System.Threading.Tasks;
using DotFramework.Infra.Web.API;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var a = "Test";

                if (context.Request.Path.Value.ToLower().Contains("value"))
                {
                    throw new Exception("Test Exception Outside of the controller");
                }
            }
            catch (Exception ex)
            {
                //if (ApiExceptionHandler.Instance.HandleException(ref ex))
                //{
                //    throw ex;
                //}

                throw;
            }

            await _next(context);
        }
    }
}
