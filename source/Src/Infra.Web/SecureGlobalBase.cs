using DotFramework.Infra.Security;
using DotFramework.Infra.Security.Model;
using System;
using System.Web;

namespace DotFramework.Infra.Web
{
    public class SecureGlobalBase : SecureGlobalServiceBase
    {
        protected override void SessionStart()
        {
            base.SessionStart();
            CreateSession();
        }

        private void CreateSession()
        {
            try
            {
                //string applicationName = ConfigurationManager.AppSettings["ApplicationName"];
                string applicationName = ApplicationManager.Instance.ApplicationCode;

                if (!String.IsNullOrEmpty(applicationName))
                {
                    CreateSessionBindingModel model = new CreateSessionBindingModel
                    {
                        IsApplicationSession = false,
                        ApplicationCode = applicationName,
                        BrowserDetail = String.Format("{0}${1}${2}", Request.Browser.Browser, Request.Browser.Version, Request.Browser.Type),
                        OperatingSystemDetail = Request.Browser.Platform,
                        UrlReferrer = (Request.UrlReferrer == null || String.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri)) ? null : Request.UrlReferrer.AbsoluteUri,
                    };

                    var sessionID = AuthService.Instance.CreateSession(model);

                    Response.Cookies.Add(new HttpCookie(SessionKey.UserSessionID, sessionID.ToString())
                    {
                        Expires = DateTime.Now.AddMinutes(30)
                    });
                }
            }
            catch
            {

            }
        }
    }
}
