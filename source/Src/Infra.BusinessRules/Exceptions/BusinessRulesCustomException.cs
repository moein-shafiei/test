using DotFramework.Infra.ExceptionHandling;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class BusinessRulesCustomException : BusinessRulesException, ICustomException
    {
        public override string RFC => "https://dotframework.net/rfc20008/business-rules-custom-exception";

        public override string Title => "Business Rules Custom Exception";

        #region Constructors

        public BusinessRulesCustomException()
        {
        }

        public BusinessRulesCustomException(string message) : base(message)
        {
        }

        public BusinessRulesCustomException(string message, Exception inner) : base(message, inner)
        {
        }

        public BusinessRulesCustomException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public BusinessRulesCustomException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public BusinessRulesCustomException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public BusinessRulesCustomException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
