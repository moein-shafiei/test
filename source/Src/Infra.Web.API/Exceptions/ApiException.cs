using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class ApiException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20016/api-exception";

        public override string Title => "API Exception";

        #region Properties

        public string ErrorContent { get; private set; }

        #endregion

        #region Constructors

        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, string errorContent) : base(message)
        {
            ErrorContent = errorContent;
        }

        public ApiException(string message, Exception inner) : base(message, inner)
        {
        }

        public ApiException(string message, string errorContent, Exception inner) : base(message, inner)
        {
            ErrorContent = errorContent;
        }

        public ApiException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public ApiException(string message, string errorContent, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
            ErrorContent = errorContent;
        }

        public ApiException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public ApiException(string message, string errorContent, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
            ErrorContent = errorContent;
        }

        public ApiException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public ApiException(string message, string errorContent, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
            ErrorContent = errorContent;
        }

        public ApiException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        public ApiException(string message, string errorContent, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
            ErrorContent = errorContent;
        }

        #endregion
    }
}
