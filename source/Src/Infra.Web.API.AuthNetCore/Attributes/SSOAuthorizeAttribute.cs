using DotFramework.Infra.Security;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DotFramework.Infra.Web.API.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SSOAuthorizeAttribute : Attribute, IAuthorizationFilter
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

        public virtual void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (String.IsNullOrEmpty(_ControllerName))
            {
                _ControllerName = ((ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;
            }

            if (String.IsNullOrEmpty(_ActionName))
            {
                _ActionName = ((ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
            }

            var isAutorized = false;
            UserDataResponseModel userData = null;
            ClientDataResponseModel clientData = null;

            try
            {
                //userData = AuthenticationProvider.Instance.Authorize(new AuthorizeRequest
                //{
                //    ControllerName = _ControllerName,
                //    ActionName = _ActionName
                //});

                if (AuthContextProvider.AuthContext?.UserType == UserTypes.User)
                {
                    userData = AuthenticationProvider.Instance.GetUserData();
                }
                if (AuthContextProvider.AuthContext?.UserType == UserTypes.Client)
                {
                    clientData = AuthenticationProvider.Instance.GetClientData();
                }

                isAutorized = true;
            }
            catch (NotImplementedException)
            {
                //isAutorized = base.IsAuthorized(actionContext);
                //TODO
                isAutorized = true;

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
                    filterContext.Result = new UnauthorizedResult();
                }
            }
            else
            {
                filterContext.Result = new UnauthorizedResult();
            }
        }
    }
}