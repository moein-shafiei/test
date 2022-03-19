using System;
using DotFramework.Infra.ExceptionHandling;

namespace DotFramework.Infra.ServiceFactory
{
    public class ServiceFactoryExceptionHandler : ExceptionHandlerBase<ServiceFactoryExceptionHandler>
    {
        private ServiceFactoryExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.ServiceFactoryPolicy, className, methodName);

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
