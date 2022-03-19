using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class RequestServiceException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20014/request-service-exception";

        public override string Title => "Request Service Exception";

        #region Constructors

        public RequestServiceException()
        {
        }

        public RequestServiceException(string message) : base(message)
        {
        }

        public RequestServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        public RequestServiceException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public RequestServiceException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public RequestServiceException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public RequestServiceException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
