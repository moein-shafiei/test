using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using DotFramework.Infra.Web.API.Auth;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using DotFramework.Infra.Request;
using DotFramework.Core;
using DotFramework.Infra;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "App started" };
        }

        [HttpGet("UserName")]
        [SSOAuthorize]
        public ActionResult<IEnumerable<string>> UserName()
        {
            var token = AuthContextProvider.AuthContext.AccessToken;
            var res = AuthContextProvider.AuthContext.UserName;
            return new string[] { token, res };
        }

        [HttpGet("test")]
        public ActionResult<double> Test([FromQuery] TestDataRequest request)
        {
            var result = RequestServiceFactory.Instance.Resolve<TestRequestService>().ProcessRequest(request);
            return result.Result;
        }
    }
}
