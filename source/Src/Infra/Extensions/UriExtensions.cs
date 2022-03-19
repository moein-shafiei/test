using System;

namespace DotFramework.Infra
{
    public static class UriExtensions
    {
        public static string GetRootUrl(this Uri uri)
        {
            return uri.AbsoluteUri.Replace(uri.AbsolutePath, String.Empty);
        }
    }
}
