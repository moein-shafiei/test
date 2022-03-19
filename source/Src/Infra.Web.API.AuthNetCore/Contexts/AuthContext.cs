using DotFramework.Core;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web.API.Auth;
using DotFramework.Infra.Web.API.Auth.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Linq;
using DotFramework.Infra.Security;

namespace DotFramework.Infra.Web.API.Auth
{
    public class AuthContext : IAuthContext
    {
        #region Internal Members
        OAuthBearerHandler oAuthBearerHandler;

        public static HttpContext Current
        {
            get
            {
                return HttpContextProvider.Current;
            }
        }

        public AuthContext()
        {
            oAuthBearerHandler = new OAuthBearerHandler();
        }

        public HttpContext Request
        {
            get
            {
                return Current;
            }
        }

        internal AuthenticationTicket AuthenticationManager { get; set; }

        public string UserType
        {
            get
            {
                return User?.Claims?.FirstOrDefault(c => c.Type == CustomClaimTypes.UserType)?.Value;
            }
        }

        public ClaimsPrincipal User
        {
            get
            {
                try
                {
                    AuthenticationManager = oAuthBearerHandler.Authenticate(Current);
                    {
                        if (AuthenticationManager != null)
                        {
                            return AuthenticationManager.Principal;
                        }
                    }
                }
                catch
                {

                }

                return null;
            }
        }


        public UserDataResponseModel UserData
        {
            get
            {
                if (Current != null && Current.Items.ContainsKey("UserData"))
                {
                    return Current.Items["UserData"] as UserDataResponseModel;
                }
                else
                {
                    return null;
                }
            }
            set { Current.Items.Add("UserData", value); }
        }

        public ClientDataResponseModel ClientData
        {
            get
            {
                if (Current != null && Current.Items.ContainsKey("ClientData"))
                {
                    return Current.Items["ClientData"] as ClientDataResponseModel;
                }
                else
                {
                    return null;
                }
            }
            set { Current.Items.Add("ClientData", value); }
        }

        //internal void SetUserData(HttpContext httpContext, object userData)
        //{
        //    httpContext.Items.Add("UserData", userData);
        //}

        //internal UserDataResponseModel GetUserData(HttpContext httpContext)
        //{
        //    if (httpContext != null && httpContext.Items.ContainsKey("UserData"))
        //    {
        //        return httpContext.Items["UserData"] as UserDataResponseModel;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #endregion

        #region Public Members

        public string AccessToken
        {
            get
            {
                return oAuthBearerHandler.GetToken(Current);
            }
        }

        public string UserName
        {
            get
            {
                if (UserData != null)
                {
                    return UserData.UserName;
                }

                return null;
            }
        }

        public string Initiator
        {
            get
            {
                if (User != null)
                {
                    return User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Initiator)?.Value;
                }

                return null;
            }
        }

        public List<String> Roles
        {
            get
            {
                if (UserData != null)
                {
                    return UserData.Roles;
                }

                return null;
            }
        }

        #endregion
    }
}