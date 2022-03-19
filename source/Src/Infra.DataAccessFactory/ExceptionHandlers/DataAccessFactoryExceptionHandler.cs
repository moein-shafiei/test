using DotFramework.Infra.ExceptionHandling;
using System;

namespace DotFramework.Infra.DataAccessFactory
{
    public class DataAccessFactoryExceptionHandler : ExceptionHandlerBase<DataAccessFactoryExceptionHandler>
    {
        private DataAccessFactoryExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.DataAccessFactoryPolicy, className, methodName);

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
