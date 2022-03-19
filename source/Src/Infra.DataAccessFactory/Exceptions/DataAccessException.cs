using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class DataAccessException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20004/data-access-exception";

        public override string Title => "Data Access Exception";

        #region Constructors

        public DataAccessException()
        {
        }

        public DataAccessException(string message) : base(message)
        {
        }

        public DataAccessException(string message, Exception inner) : base(message, inner)
        {
        }

        public DataAccessException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public DataAccessException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public DataAccessException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public DataAccessException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
