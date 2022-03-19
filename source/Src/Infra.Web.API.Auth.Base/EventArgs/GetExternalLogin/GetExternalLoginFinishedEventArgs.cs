using System;
#if NETFRAMEWORK
using System.Web.Http.Results;
#else
using Microsoft.AspNetCore.Mvc;
#endif

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GetExternalLoginFinishedEventArgs : EventArgs
    {
        public readonly string Provider;
        public readonly string RedirectUrl;
        public readonly RedirectResult RedirectResult;

        public GetExternalLoginFinishedEventArgs(string provider, string redirect_url, RedirectResult redirectResult)
        {
            Provider = provider;
            RedirectUrl = redirect_url;
            RedirectResult = redirectResult;
        }
    } 
}