using DotFramework.Core.DependencyInjection;
using System;

namespace DotFramework.Infra.Request
{
    public class DataServiceFactory : VirtualMethodFactoryBase<DataServiceFactory, DataServiceBase, DataServiceInterceptionBehavior>
    {
        protected override bool HandleException(ref Exception ex)
        {
            return true;
        }
    }
}
