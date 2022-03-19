using System.Collections.Generic;
using System.Linq;

namespace System.Security.Claims
{
    public static class ClaimsIdentityExtensions
    {
        public static string TryGetValue(this ClaimsIdentity identity, string type)
        {
            if (identity == null)
            {
                return null;
            }

            if (identity.Claims.Count(c => c.Type == type) != 0)
            {
                return identity.Claims.First(c => c.Type == type).Value;
            }
            else
            {
                return null;
            }
        }

        public static List<String> TryGetValues(this ClaimsIdentity identity, string type)
        {
            if (identity.Claims.Count(c => c.Type == type) != 0)
            {
                return identity.Claims.Where(c => c.Type == type).Select(c => c.Value).ToList();
            }
            else
            {
                return new List<String>();
            }
        }
    }
}
