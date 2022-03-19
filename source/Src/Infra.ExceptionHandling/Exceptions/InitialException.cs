using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class InitialException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc10001/initial-exception";

        public override string Title => "Initial Exception";

        public InitialException()
        {
        }

        public InitialException(string message) : base(message)
        {
        }

        public InitialException(string message, Exception inner) : base(message, inner)
        {
        }

        public InitialException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public InitialException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public InitialException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public InitialException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }
    }
}
