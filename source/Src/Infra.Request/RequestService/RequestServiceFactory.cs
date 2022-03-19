using DotFramework.Core.DependencyInjection;
using System;

namespace DotFramework.Infra.Request
{
    public class RequestServiceFactory : VirtualMethodFactoryBase<RequestServiceFactory, RequestServiceBase, RequestServiceInterceptionBehavior>
    {
        protected override bool HandleException(ref Exception ex)
        {
            return true;
        }
    }
}
