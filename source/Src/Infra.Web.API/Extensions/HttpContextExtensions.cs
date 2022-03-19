using System;
using System.Text.RegularExpressions;
using System.Web;

namespace DotFramework.Web.API
{
    public static class HttpContextExtensions
    {
        public static string GetToken(this HttpContext context)
        {
            try
            {
                var authorizeHeader = context.Request.Headers["Authorization"];

                if (authorizeHeader != String.Empty)
                {
                    var authType = "Bearer";
                    string token = authorizeHeader;

                    if (token?.StartsWith(authType, StringComparison.OrdinalIgnoreCase) ?? false)
                    {
                        var regEx = new Regex(authType, RegexOptions.IgnoreCase);
                        token = regEx.Replace(token, string.Empty).TrimStart();

                        return token;
                    }

                    authType = "Basic";

                    if (token?.StartsWith(authType, StringComparison.OrdinalIgnoreCase) ?? false)
                    {
                        var regEx = new Regex(authType, RegexOptions.IgnoreCase);
                        token = regEx.Replace(token, string.Empty).TrimStart();

                        return token;
                    }
                }

                throw new NotSupportedException($"Unsupported token type");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
