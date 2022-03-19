using System;
using DotFramework.Core;
using DotFramework.Infra.ExceptionHandling;

namespace DotFramework.Infra.Request
{
    public class RequestServiceExceptionHandler : ExceptionHandlerBase<RequestServiceExceptionHandler>
    {
        private RequestServiceExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            if (ex is RequestServiceCustomException)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.RequestServiceCustomPolicy, className, methodName);
                ex = new RequestServiceCustomException(ex.Message, ex);
            }
            else if (ex is ExceptionBase)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.PassThroughPolicy, className, methodName);
                ex = new PassThroughException(ex.Message, ex);
            }
            else
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.RequestServicePolicy, className, methodName);
            }

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
