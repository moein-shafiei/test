using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class BusinessRulesException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20007/business-rules-exception";

        public override string Title => "Business Rules Exception";

        #region Constructors

        public BusinessRulesException()
        {
        }

        public BusinessRulesException(string message) : base(message)
        {
        }

        public BusinessRulesException(string message, Exception inner) : base(message, inner)
        {
        }

        public BusinessRulesException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public BusinessRulesException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public BusinessRulesException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public BusinessRulesException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
