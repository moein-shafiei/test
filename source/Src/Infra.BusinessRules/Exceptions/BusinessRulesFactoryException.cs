using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class BusinessRulesFactoryException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20006/business-rules-factory-exception";

        public override string Title => "Business Rules Factory Exception";

        #region Constructors

        public BusinessRulesFactoryException()
        {
        }

        public BusinessRulesFactoryException(string message) : base(message)
        {
        }

        public BusinessRulesFactoryException(string message, Exception inner) : base(message, inner)
        {
        }

        public BusinessRulesFactoryException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public BusinessRulesFactoryException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public BusinessRulesFactoryException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public BusinessRulesFactoryException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
