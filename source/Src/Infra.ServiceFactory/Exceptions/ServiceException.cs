using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class ServiceException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20010/service-exception";

        public override string Title => "Service Exception";

        #region Constructors

        public ServiceException()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        public ServiceException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public ServiceException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public ServiceException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public ServiceException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
