using DotFramework.Infra.ExceptionHandling;
using System;
using Unity.Interception.PolicyInjection.Pipeline;

namespace DotFramework.Infra.DataAccessFactory
{
    public class DataAccessInterceptionBehavior : ExceptionHandlerInterceptionBehavior
    {
        public override bool HandleException(ref Exception ex, IMethodInvocation input)
        {
            return DataAccessExceptionHandler.Instance.HandleException(ref ex, input.Target.GetType().FullName, input.MethodBase.Name);
        }
    }
}
