using DotFramework.Core;
using DotFramework.Infra.Web.API.Auth.Base.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Web.API.Auth.Base.Providers
{
    public class TokenProviderFactory<TTokenProviderFactory> : SingletonProvider<TTokenProviderFactory>
        where TTokenProviderFactory : class
    {
        public void Configure(ITokenProvider tokenProvider)
        {
            TokenProvider = tokenProvider;
        }

        public ITokenProvider TokenProvider { get; private set; }
    }

    public class TokenProviderFactory : TokenProviderFactory<TokenProviderFactory>
    {

    }
}
