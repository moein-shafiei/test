using DotFramework.Infra.ExceptionHandling;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class ServiceCustomException : ServiceException, ICustomException
    {
        public override string RFC => "https://dotframework.net/rfc20011/service-custom-exception";

        public override string Title => "Service Custom Exception";

        #region Constructors

        public ServiceCustomException()
        {
        }

        public ServiceCustomException(string message) : base(message)
        {
        }

        public ServiceCustomException(string message, Exception inner) : base(message, inner)
        {
        }

        public ServiceCustomException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public ServiceCustomException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public ServiceCustomException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public ServiceCustomException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
