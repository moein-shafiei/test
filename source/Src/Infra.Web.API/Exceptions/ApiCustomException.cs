using DotFramework.Core;
using DotFramework.Infra.ExceptionHandling;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class ApiCustomException : ExceptionBase, ICustomException
    {
        public override string RFC => "https://dotframework.net/rfc20017/api-custom-exception";

        public override string Title => "API Custom Exception";

        #region Constructors

        public ApiCustomException()
        {
        }

        public ApiCustomException(string message) : base(message)
        {
        }

        public ApiCustomException(string message, Exception inner) : base(message, inner)
        {
        }

        public ApiCustomException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public ApiCustomException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public ApiCustomException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public ApiCustomException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
