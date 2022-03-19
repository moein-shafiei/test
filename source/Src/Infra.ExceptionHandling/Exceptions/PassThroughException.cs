using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class PassThroughException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20001/pass-through-exception";

        public override string Title => "Pass Through Exception";

        #region Constructors

        public PassThroughException()
        {
        }

        public PassThroughException(string message) : base(message)
        {
        }

        public PassThroughException(string message, Exception inner) : base(message, inner)
        {
        }

        public PassThroughException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public PassThroughException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public PassThroughException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public PassThroughException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
