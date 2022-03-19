using DotFramework.Core.Configuration;
using DotFramework.Core.Web;
using DotFramework.Infra.Security;
using DotFramework.Infra.Security.Model;
using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DotFramework.Infra.Web
{
    public class SecureGlobalServiceBase : GlobalServiceBase
    {
        protected virtual bool NeedApplicationSession
        {
            get
            {
                return true;
            }
        }

        protected virtual bool NeedAuthenticationService
        {
            get
            {
                return true;
            }
        }

        protected override void Configure()
        {
            base.Configure();

            if (NeedAuthenticationService)
            {
                AuthService.Instance.Initialize(
                    AppSettingsManager.Instance.Get("AuthEndpointPath"),
                    AppSettingsManager.Instance.Get("ClientID"),
                    AppSettingsManager.Instance.Get("ClientSecret"));
            }

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            this.CreateApplicationSession();
        }

        private AuthorizationToken GetToken()
        {
            //if (String.IsNullOrEmpty(CoreConstants.SSOTokenID))
            //{
            //    throw new UnauthorizedAccessException();
            //}

            string token = null;

            if (OperationContext.Current != null)
            {
                HttpRequestMessageProperty requestMessageProperty = OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;

                if (requestMessageProperty != null)
                {
                    token = requestMessageProperty.Headers["AuthenticationToken"];
                }
            }
            //else if (HttpContext.Current != null)
            //{
            //    if (HttpContext.Current.Request != null)
            //    {
            //        //if (HttpContext.Current.Request.Cookies[CoreConstants.SSOTokenID] != null)
            //        //{
            //        //    token = HttpContext.Current.Request.Cookies[CoreConstants.SSOTokenID].Value;
            //        //}
            //        //else if (!String.IsNullOrEmpty(HttpContext.Current.Request.Headers["Authorization"]))
            //        //{
            //        //    token = AuthenticationHeaderValue.Parse(HttpContext.Current.Request.Headers["Authorization"]).Parameter;
            //        //}
            //    }
            //}

            if (String.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                return new AuthorizationToken(TokenTypeEnum.Bearer, token);
            }
        }

        protected async void CreateApplicationSession()
        {
            if (NeedApplicationSession)
            {
                try
                {
                    string applicationName = ApplicationManager.Instance.ApplicationCode;

                    if (!String.IsNullOrEmpty(applicationName))
                    {
                        CreateSessionBindingModel model = new CreateSessionBindingModel
                        {
                            IsApplicationSession = true,
                            ApplicationCode = applicationName
                        };

                        UserSessionManager.Instance.ApplicationSessionID = await AuthService.Instance.CreateSessionAsync(model);
                    }
                }
                catch
                {

                }
            }
        }
    }
}
