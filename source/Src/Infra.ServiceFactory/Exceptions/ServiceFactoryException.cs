using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class ServiceFactoryException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20009/service-factory-exception";

        public override string Title => "Service Factory Exception";

        #region Constructors

        public ServiceFactoryException()
        {
        }

        public ServiceFactoryException(string message) : base(message)
        {
        }

        public ServiceFactoryException(string message, Exception inner) : base(message, inner)
        {
        }

        public ServiceFactoryException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public ServiceFactoryException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public ServiceFactoryException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public ServiceFactoryException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
