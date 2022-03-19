using System;
using System.Threading.Tasks;
using DotFramework.Infra.Web.API.Auth;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApplication2.App_Start.Startup))]
namespace WebApplication2.App_Start
{
    public class Startup : SecureWebAPIStartup
    {
        public void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
        }
    }
}
