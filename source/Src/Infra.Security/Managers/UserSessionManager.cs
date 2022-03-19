using DotFramework.Core;
using System;

namespace DotFramework.Infra.Security
{
    public class UserSessionManager : SingletonProvider<UserSessionManager>
    {
        private UserSessionManager()
        {

        }

        public Int64 ApplicationSessionID { get; set; }

        public Int64 GetSessionID()
        {
            Int64 sessionID = 0;

            //try
            //{
            //    if (OperationContext.Current != null)
            //    {
            //        HttpRequestMessageProperty requestMessageProperty = OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;

            //        if (requestMessageProperty != null)
            //        {
            //            sessionID = requestMessageProperty.Headers[SessionKey.UserSessionID].ToInt64();
            //        }
            //    }
            //    else if (HttpContext.Current != null)
            //    {
            //        if (HttpContext.Current.Request != null)
            //        {
            //            if (HttpContext.Current.Request.Cookies[SessionKey.UserSessionID] != null)
            //            {
            //                sessionID = HttpContext.Current.Request.Cookies[SessionKey.UserSessionID].Value.ToInt64();
            //            }
            //            else if (!String.IsNullOrEmpty(HttpContext.Current.Request.Headers[SessionKey.UserSessionID]))
            //            {
            //                sessionID = HttpContext.Current.Request.Headers[SessionKey.UserSessionID].ToInt64();
            //            }
            //            else
            //            {
            //                sessionID = this.ApplicationSessionID;
            //            }
            //        }
            //    }
            //}
            //catch (HttpException)
            //{
            //    sessionID = this.ApplicationSessionID;
            //}
            //catch
            //{
            //    throw;
            //}

            return sessionID;
        }
    }
}
