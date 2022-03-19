using System;
using DotFramework.Infra.ExceptionHandling;

namespace DotFramework.Infra.BusinessRules
{
    public class BusinessRulesFactoryExceptionHandler : ExceptionHandlerBase<BusinessRulesFactoryExceptionHandler>
    {
        private BusinessRulesFactoryExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.BusinessRulesFactoryPolicy, className, methodName);

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
