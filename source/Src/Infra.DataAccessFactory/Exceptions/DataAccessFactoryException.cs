using DotFramework.Core;
using System;
using System.Reflection;

namespace DotFramework.Infra
{
    public class DataAccessFactoryException : ExceptionBase
    {
        public override string RFC => "https://dotframework.net/rfc20003/data-access-factory-exception";

        public override string Title => "Data Access Factory Exception";

        #region Constructors

        public DataAccessFactoryException()
        {
        }

        public DataAccessFactoryException(string message) : base(message)
        {
        }

        public DataAccessFactoryException(string message, Exception inner) : base(message, inner)
        {
        }

        public DataAccessFactoryException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public DataAccessFactoryException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public DataAccessFactoryException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public DataAccessFactoryException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        #endregion
    }
}
