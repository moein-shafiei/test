using System;
using DotFramework.Core;
using DotFramework.Infra.ExceptionHandling;

namespace DotFramework.Infra.ServiceFactory
{
    public class ServiceExceptionHandler : ExceptionHandlerBase<ServiceExceptionHandler>
    {
        private ServiceExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            if (ex is ServiceCustomException || ex is UnauthorizedAccessException)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.ServiceCustomPolicy, className, methodName);
                ex = new ServiceCustomException(ex.Message, ex);
            }
            else if (ex is ExceptionBase)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.PassThroughPolicy, className, methodName);
                ex = new PassThroughException(ex.Message, ex);
            }
            else
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.ServicePolicy, className, methodName);
            }

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
