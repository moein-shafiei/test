using DotFramework.Core;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;

namespace DotFramework.Infra.ExceptionHandling
{
    public class TraceLogManager : SingletonProvider<TraceLogManager>
    {
        #region Constructor

        private TraceLogManager()
        {
            IConfigurationSource config = ConfigurationSourceFactory.Create();

            LogWriterFactory logWriterFactory = new LogWriterFactory(config);
            LogWriter logWriter = logWriterFactory.Create();
            Logger.SetLogWriter(logWriter);

            ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
            ExceptionManager exManager = factory.CreateManager();
            ExceptionPolicy.SetExceptionManager(exManager);
        }

        #endregion

        #region Variables

        private string _ApplicationCode;

        #endregion

        #region Initialize

        public void Initialize(string applicationCode)
        {
            _ApplicationCode = applicationCode;
        }

        #endregion

        #region Exception Handling

        public bool HandleException(Exception ex, string policyName)
        {
            return HandleException(ex, policyName, String.Empty, String.Empty);
        }

        public bool HandleException(Exception ex, string policyName, string className, string methodName)
        {
            ExceptionBase exception;

            if (ex is ExceptionBase)
            {
                exception = (ExceptionBase)ex;

                exception.ApplicationCode = _ApplicationCode;
                exception.ClassName = className;
                exception.MethodName = methodName;
            }
            else
            {
                exception = new InitialException(ex.Message, ex, _ApplicationCode, className, methodName);
            }

            return ExceptionPolicy.HandleException(exception, policyName);
        }

        #endregion
    }
}
