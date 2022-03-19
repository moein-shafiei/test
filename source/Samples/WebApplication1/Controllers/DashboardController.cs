using DotFramework.Infra.Web.API.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SSOAuthorize]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] GetDashboardRequest request)
        {
            var response = new GetDashboardResponse { Message = $"This is dashboard! ({request.Name})" };
            return Ok(response);
        }
    }

    public class GetDashboardRequest
    {
        public string Name { get; set; }
    }

    public class GetDashboardResponse
    {
        public string Message { get; set; }
    }
}
