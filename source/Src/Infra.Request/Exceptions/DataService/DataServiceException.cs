using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class DataServiceException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20012/data-service-exception";

        public override string Title => "Data Service Exception";

        #region Constructors

        public DataServiceException()
        {
        }

        public DataServiceException(string message) : base(message)
        {
        }

        public DataServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        public DataServiceException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public DataServiceException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public DataServiceException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public DataServiceException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
