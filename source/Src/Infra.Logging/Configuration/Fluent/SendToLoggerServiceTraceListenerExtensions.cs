using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System;
using System.Diagnostics;

namespace DotFramework.Infra.Logging.Configuration
{
    /// <summary>
    /// Extension methods to support configuration of <see cref="LoggerServiceTraceListener"/>.
    /// </summary>
    /// <seealso cref="LoggerServiceTraceListener"/>
    /// <seealso cref="LoggerServiceTraceListenerData"/>
    public static class SendToLoggerServiceTraceListenerExtensions
    {
        /// <summary>
        /// Adds a new <see cref="LoggerServiceTraceListener"/> to the logging settings and creates
        /// a reference to this Trace Listener for the current category source.
        /// </summary>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="listenerName">The name of the <see cref="LoggerServiceTraceListener"/>.</param>
        /// <returns>Fluent interface that can be used to further configure the created <see cref="LoggerServiceTraceListenerData"/>. </returns>
        /// <seealso cref="LoggerServiceTraceListener"/>
        /// <seealso cref="LoggerServiceTraceListenerData"/>
        public static ILoggingConfigurationSendToLoggerServiceTraceListener Database(this ILoggingConfigurationSendTo context, string listenerName)
        {
            if (string.IsNullOrEmpty(listenerName))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "listenerName");
            }

            return new SendToLoggerServiceTraceListenerBuilder(context, listenerName);
        }

        private class SendToLoggerServiceTraceListenerBuilder : SendToTraceListenerExtension, ILoggingConfigurationSendToLoggerServiceTraceListener
        {
            LoggerServiceTraceListenerData LoggerServiceTraceListener;
            public SendToLoggerServiceTraceListenerBuilder(ILoggingConfigurationSendTo context, string listenerName)
                : base(context)
            {
                LoggerServiceTraceListener = new LoggerServiceTraceListenerData
                {
                    Name = listenerName
                };

                base.AddTraceListenerToSettingsAndCategory(LoggerServiceTraceListener);
            }


            public ILoggingConfigurationSendToLoggerServiceTraceListener FormatWith(IFormatterBuilder formatBuilder)
            {
                if (formatBuilder == null)
                {
                    throw new ArgumentNullException("formatBuilder");
                }

                FormatterData formatter = formatBuilder.GetFormatterData();
                LoggerServiceTraceListener.Formatter = formatter.Name;
                LoggingSettings.Formatters.Add(formatter);

                return this;
            }

            public ILoggingConfigurationSendToLoggerServiceTraceListener FormatWithSharedFormatter(string formatterName)
            {
                LoggerServiceTraceListener.Formatter = formatterName;

                return this;
            }

            public ILoggingConfigurationSendToLoggerServiceTraceListener WithTraceOptions(TraceOptions traceOptions)
            {
                LoggerServiceTraceListener.TraceOutputOptions = traceOptions;

                return this;
            }

            public ILoggingConfigurationSendToLoggerServiceTraceListener Filter(SourceLevels sourceLevel)
            {
                LoggerServiceTraceListener.Filter = sourceLevel;

                return this;
            }

            public ILoggingConfigurationSendToLoggerServiceTraceListener WithWriteLogEndpointAddress(string writeLogEndpointAddress)
            {
                if (string.IsNullOrEmpty(writeLogEndpointAddress))
                {
                    throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "writeLogEndpointAddress");
                }

                LoggerServiceTraceListener.WriteLogEndpointAddress = writeLogEndpointAddress;

                return this;
            }
        }
    }
}
