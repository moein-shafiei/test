using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DotFramework.Infra.Web.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        //[Route("/error-local-development")]
        //public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        //{
        //    if (!webHostEnvironment.IsDevelopment())
        //    {
        //        throw new InvalidOperationException(
        //            "This shouldn't be invoked in non-development environments.");
        //    }

        //    var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        //    return Problem(
        //        detail: context.Error.StackTrace,
        //        title: context.Error.Message);
        //}

        [Route("/error")]
        [HttpGet]
        public IActionResult Error() => Problem();
    }
}
