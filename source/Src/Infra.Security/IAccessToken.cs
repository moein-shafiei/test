using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security
{
    public interface IAccessToken
    {
        string AccessToken { get; set; }

        string TokenType { get; set; }

        string Initiator { get; set; }
    }
}
