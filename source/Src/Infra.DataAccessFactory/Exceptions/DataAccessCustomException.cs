using DotFramework.Infra.ExceptionHandling;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class DataAccessCustomException : DataAccessException, ICustomException
    {
        public override string RFC => "https://dotframework.net/rfc20005/data-access-custom-exception";

        public override string Title => "Data Access Custom Exception";

        #region Constructors

        public DataAccessCustomException()
        {
        }

        public DataAccessCustomException(string message) : base(message)
        {
        }

        public DataAccessCustomException(string message, Exception inner) : base(message, inner)
        {
        }

        public DataAccessCustomException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public DataAccessCustomException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public DataAccessCustomException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public DataAccessCustomException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
