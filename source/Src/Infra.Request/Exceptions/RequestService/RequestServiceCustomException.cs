using DotFramework.Infra.ExceptionHandling;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class RequestServiceCustomException : RequestServiceException, ICustomException
    {
        public override string RFC => "https://dotframework.net/rfc20015/request-service-custom-exception";

        public override string Title => "Request Service Custom Exception";

        #region Constructors

        public RequestServiceCustomException()
        {
        }

        public RequestServiceCustomException(string message) : base(message)
        {
        }

        public RequestServiceCustomException(string message, Exception inner) : base(message, inner)
        {
        }

        public RequestServiceCustomException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public RequestServiceCustomException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public RequestServiceCustomException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public RequestServiceCustomException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
