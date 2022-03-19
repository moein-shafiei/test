using DotFramework.Core;
using DotFramework.Infra.Security;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web.API.Auth.Base;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;

namespace DotFramework.Infra.Web.API.Auth
{
    public class AuthContext : IAuthContext
    {
        #region Internal Members

        internal IOwinContext OwinContext
        {
            get
            {
                return HttpContext.Current.Request.GetOwinContext();
            }
        }

        internal IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (OwinContext != null)
                {
                    return OwinContext.Authentication;
                }

                return null;
            }
        }

        public string UserType
        {
            get
            {
                return User?.Claims?.FirstOrDefault(c=> c.Type == CustomClaimTypes.UserType)?.Value;
            }
        }

        public ClaimsPrincipal User
        {
            get
            {
                if (AuthenticationManager != null)
                {
                    return AuthenticationManager.User;
                }

                return null;
            }
        }

        public HttpRequestMessage Request
        {
            get
            {
                return HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            }
        }

        internal AuthenticationHeaderValue Authorization
        {
            get
            {
                if (Request != null)
                {
                    return Request.Headers.Authorization;
                }

                return null;
            }
        }

        public UserDataResponseModel UserData
        {
            get
            {
                if (Request != null && Request.Properties.ContainsKey("UserData"))
                {
                    return Request.Properties["UserData"] as UserDataResponseModel;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (Request.Properties.ContainsKey("UserData"))
                {
                    Request.Properties["UserData"] = value;
                }
                else
                {
                    Request.Properties.Add("UserData", value);
                }
            }
        }

        public ClientDataResponseModel ClientData
        {
            get
            {
                if (Request != null && Request.Properties.ContainsKey("ClientData"))
                {
                    return Request.Properties["ClientData"] as ClientDataResponseModel;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (Request.Properties.ContainsKey("ClientData"))
                {
                    Request.Properties["ClientData"] = value;
                }
                else
                {
                    Request.Properties.Add("ClientData", value);
                }
            }
        }

        #endregion

        #region Public Members

        public string AccessToken
        {
            get
            {
                if (Authorization != null)
                {
                    return Authorization.Parameter;
                }

                return null;
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
                    return User.Claims.FirstOrDefault(c=> c.Type == CustomClaimTypes.Initiator)?.Value;
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