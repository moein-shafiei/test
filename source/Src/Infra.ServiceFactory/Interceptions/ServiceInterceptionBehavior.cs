using DotFramework.Infra.ExceptionHandling;
using System;
using Unity.Interception.PolicyInjection.Pipeline;

namespace DotFramework.Infra.ServiceFactory
{
    public class ServiceInterceptionBehavior : ExceptionHandlerInterceptionBehavior
    {
        public override bool HandleException(ref Exception ex, IMethodInvocation input)
        {
            return ServiceExceptionHandler.Instance.HandleException(ref ex, input.Target.GetType().FullName, input.MethodBase.Name);
        }
    }
}
