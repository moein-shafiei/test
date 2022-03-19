using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class CriticalException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20002/critical-exception";

        public override string Title => "Critical Exception";

        #region Constructors

        public CriticalException()
        {
        }

        public CriticalException(string message) : base(message)
        {
        }

        public CriticalException(string message, Exception inner) : base(message, inner)
        {
        }

        public CriticalException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public CriticalException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public CriticalException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public CriticalException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
