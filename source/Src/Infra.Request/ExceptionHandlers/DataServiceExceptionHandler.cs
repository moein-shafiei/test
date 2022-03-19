using System;
using DotFramework.Core;
using DotFramework.Infra.ExceptionHandling;

namespace DotFramework.Infra.Request
{
    public class DataServiceExceptionHandler : ExceptionHandlerBase<DataServiceExceptionHandler>
    {
        private DataServiceExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            if (ex is DataServiceCustomException)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.DataServiceCustomPolicy, className, methodName);
                ex = new DataServiceCustomException(ex.Message, ex);
            }
            else if (ex is ExceptionBase)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.PassThroughPolicy, className, methodName);
                ex = new PassThroughException(ex.Message, ex);
            }
            else
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.DataServicePolicy, className, methodName);
            }

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
