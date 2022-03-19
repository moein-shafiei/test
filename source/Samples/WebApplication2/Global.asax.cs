using DotFramework.Infra.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApplication2
{
    public class WebApiApplication : SecureGlobalServiceBase
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            this.ApplicationStart();
        }

        protected void Application_BeginRequest()
        {
        }

        protected void Session_Start()
        {
            this.SessionStart();
        }

        protected void Session_End()
        {
            this.SessionEnd();
        }

        protected void Application_End()
        {
            this.ApplicationEnd();
        }

        protected void Application_Error()
        {
            this.ApplicationEnd();
        }
    }
}
