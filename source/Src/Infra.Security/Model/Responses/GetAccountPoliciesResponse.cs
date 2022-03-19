using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class GetAccountPoliciesResponse
    {
        public IEnumerable<String> PolicyNames { get; set; }
    }
}
