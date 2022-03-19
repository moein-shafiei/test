using System;
using System.Data.SqlClient;
using DotFramework.Infra.ExceptionHandling;

namespace DotFramework.Infra.DataAccessFactory
{
    public class DataAccessExceptionHandler : ExceptionHandlerBase<DataAccessExceptionHandler>
    {
        private DataAccessExceptionHandler()
        {

        }

        public override bool HandleException(ref Exception ex, string className, string methodName)
        {
            bool reThrow = false;

            if (ex is DataAccessCustomException)
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.DataAccessCustomPolicy, className, methodName);
                ex = new DataAccessCustomException(ex.Message, ex);
            }
            else if ((ex is SqlException))
            {
                SqlException dbExp = ex as SqlException;

                if (dbExp.Number >= 50000)
                {
                    reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.DataAccessCustomPolicy, className, methodName);
                    ex = new DataAccessCustomException(ex.Message);
                }
                else
                {
                    reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.DataAccessPolicy, className, methodName);
                }
            }
            else
            {
                reThrow = TraceLogManager.Instance.HandleException(ex, ExceptionHandlingPolicyConstants.DataAccessPolicy, className, methodName);
            }

            if (reThrow)
            {
                throw ex;
            }

            return reThrow;
        }
    }
}
