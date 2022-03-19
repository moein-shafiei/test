using DotFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Web.API.Auth.Base.Providers
{
    public class AuthContextProvider
    {
        private static IAuthContext _authContext;

        public static void Configure(IAuthContext authContext)
        {
            _authContext = authContext;
        }

        public static IAuthContext AuthContext
        {
            get
            {
                return _authContext;
            }
        }
    }
}
