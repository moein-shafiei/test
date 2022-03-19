using DotFramework.Infra.ExceptionHandling;
using System;
using Unity.Interception.PolicyInjection.Pipeline;

namespace DotFramework.Infra.Request
{
    public class DataServiceInterceptionBehavior : ExceptionHandlerInterceptionBehavior
    {
        public override bool HandleException(ref Exception ex, IMethodInvocation input)
        {
            return DataServiceExceptionHandler.Instance.HandleException(ref ex, input.MethodBase.DeclaringType.FullName, input.MethodBase.Name);
        }
    }
}
