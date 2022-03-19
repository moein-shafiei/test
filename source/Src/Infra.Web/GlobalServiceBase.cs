using DotFramework.Infra.Configuration;
using DotFramework.Infra.ExceptionHandling;
using System;
using System.Web;
using System.Web.Configuration;

namespace DotFramework.Infra.Web
{
    public class GlobalServiceBase : HttpApplication
    {
        protected virtual bool NeedApplicationConfiguration
        {
            get
            {
                return true;
            }
        }

        protected virtual void Configure()
        {
            TraceLogManager.Instance.Initialize(ApplicationManager.Instance.ApplicationCode);

            if (NeedApplicationConfiguration)
            {
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(ReverseMapPath(HttpRuntime.AppDomainAppPath));
                ApplicationConfigurator.Configure(config);
            }
        }

        protected virtual void ApplicationStart()
        {
            Configure();
        }

        protected virtual void SessionStart()
        {

        }

        protected virtual void SessionEnd()
        {

        }

        protected virtual void ApplicationEnd()
        {

        }

        protected virtual void ApplicationError()
        {

        }

        private string ReverseMapPath(string path)
        {
            string appPath = HttpContext.Current.Server.MapPath("~");
            string res = string.Format("~{0}", path.Replace(appPath, "").Replace("\\", "/"));
            return res;
        }

        protected class FirstRequestInitialization
        {
            private static bool s_InitializedAlready = false;
            private static Object s_lock = new Object();

            public static void Initialize(Action action)
            {
                Initialize(HttpContext.Current, action);
            }

            public static void Initialize(HttpContext context, Action action)
            {
                if (s_InitializedAlready)
                {
                    return;
                }

                lock (s_lock)
                {
                    if (s_InitializedAlready)
                    {
                        return;
                    }

                    action();
                    s_InitializedAlready = true;
                }
            }
        }
    }
}
