using System;

namespace DotFramework.Infra.Web.API.Auth.Base
{
    public class GetExternalLoginStartedEventArgs : EventArgs
    {
        public readonly string Provider;
        public readonly string RedirectUrl;

        public GetExternalLoginStartedEventArgs(string provider, string redirect_url)
        {
            Provider = provider;
            RedirectUrl = redirect_url;
        }
    }
}