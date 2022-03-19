using DotFramework.Infra.Security;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using System;
using System.Web.Http;
using System.Web.Http.Controllers; 

namespace DotFramework.Infra.Web.API.Auth
{
    public class SSOAuthorizeAttribute : AuthorizeAttribute
    {
        private string _ControllerName;
        private string _ActionName;

        public SSOAuthorizeAttribute() : base()
        {

        }

        public SSOAuthorizeAttribute(string controllerName, string actionName) : base()
        {
            _ControllerName = controllerName;
            _ActionName = actionName;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (String.IsNullOrEmpty(_ControllerName))
            {
                _ControllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            }

            if (String.IsNullOrEmpty(_ActionName))
            {
                _ActionName = actionContext.ActionDescriptor.ActionName;
            }

            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAutorized = false;
            UserDataResponseModel userData = null;
            ClientDataResponseModel clientData = null;

            try
            {
                userData = AuthenticationProvider.Instance.Authorize(new AuthorizeRequest
                {
                    ControllerName = _ControllerName,
                    ActionName = _ActionName
                });

                isAutorized = true;
            }
            catch (NotImplementedException)
            {
                isAutorized = base.IsAuthorized(actionContext);

                if (isAutorized)
                {
                    if (AuthContextProvider.AuthContext?.UserType == UserTypes.User)
                    {
                        userData = AuthenticationProvider.Instance.GetUserData();
                    }
                    if (AuthContextProvider.AuthContext?.UserType == UserTypes.Client)
                    {
                        clientData = AuthenticationProvider.Instance.GetClientData();
                    }
                }
            }
            catch
            {
                isAutorized = false;
            }

            if (isAutorized)
            {
                if (userData != null)
                {
                    AuthContextProvider.AuthContext.UserData = userData;
                }
                else if (clientData != null)
                {
                    AuthContextProvider.AuthContext.ClientData = clientData;
                }
                else
                {
                    isAutorized = false;
                }
            }

            return isAutorized;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }
    } 
}