using DotFramework.Infra.ExceptionHandling;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class DataServiceCustomException : DataServiceException, ICustomException
    {
        public override string RFC => "https://dotframework.net/rfc20013/data-service-custom-exception";

        public override string Title => "Data Service Custom Exception";

        #region Constructors

        public DataServiceCustomException()
        {
        }

        public DataServiceCustomException(string message) : base(message)
        {
        }

        public DataServiceCustomException(string message, Exception inner) : base(message, inner)
        {
        }

        public DataServiceCustomException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public DataServiceCustomException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public DataServiceCustomException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public DataServiceCustomException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
