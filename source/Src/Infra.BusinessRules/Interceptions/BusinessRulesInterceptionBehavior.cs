using DotFramework.Infra.ExceptionHandling;
using System;
using Unity.Interception.PolicyInjection.Pipeline;

namespace DotFramework.Infra.BusinessRules
{
    public class BusinessRulesInterceptionBehavior : ExceptionHandlerInterceptionBehavior
    {
        public override bool HandleException(ref Exception ex, IMethodInvocation input)
        {
            return BusinessRulesExceptionHandler.Instance.HandleException(ref ex, input.MethodBase.DeclaringType.FullName, input.MethodBase.Name);
        }
    }
}
