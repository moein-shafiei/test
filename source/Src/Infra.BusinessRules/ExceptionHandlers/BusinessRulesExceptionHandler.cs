using System;
using DotFramework.Core;
using DotFramework.Infra.ExceptionHandling;

namespace DotFramework.Infra.BusinessRules
{
    public class BusinessRulesExceptionHandler : ExceptionHandlerBase<BusinessRulesExceptionHandler>
    {
        private BusinessRulesExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            if (ex is BusinessRulesCustomException)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.BusinessRulesCustomPolicy, className, methodName);
                ex = new BusinessRulesCustomException(ex.Message, ex);
            }
            else if (ex is ExceptionBase)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.PassThroughPolicy, className, methodName);
                ex = new PassThroughException(ex.Message, ex);
            }
            else
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.BusinessRulesPolicy, className, methodName);
            }

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
