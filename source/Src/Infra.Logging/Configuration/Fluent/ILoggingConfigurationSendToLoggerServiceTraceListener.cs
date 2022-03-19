using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using System.Diagnostics;

namespace DotFramework.Infra.Logging.Configuration
{
    /// <summary>
    /// Fluent interface used to configure a <see cref="LoggerServiceTraceListener"/> instance.
    /// </summary>
    /// <seealso cref="LoggerServiceTraceListener"/>
    /// <seealso cref="LoggerServiceTraceListenerData"/>
    public interface ILoggingConfigurationSendToLoggerServiceTraceListener : ILoggingConfigurationContd, ILoggingConfigurationCategoryContd, IFluentInterface
    {

        /// <summary>
        /// Specifies the formatter used to format database log messages send by this <see cref="LoggerServiceTraceListener"/>.<br/>
        /// </summary>
        /// <param name="formatBuilder">The <see cref="FormatterBuilder"/> used to create an <see cref="LogFormatter"/> .</param>
        /// <returns>Fluent interface that can be used to further configure the current <see cref="LoggerServiceTraceListener"/> instance. </returns>
        /// <seealso cref="LoggerServiceTraceListener"/>
        /// <seealso cref="LoggerServiceTraceListenerData"/>
        ILoggingConfigurationSendToLoggerServiceTraceListener FormatWith(IFormatterBuilder formatBuilder);


        /// <summary>
        /// Specifies the formatter used to format log messages send by this <see cref="LoggerServiceTraceListener"/>.<br/>
        /// </summary>
        /// <returns>Fluent interface that can be used to further configure the current <see cref="LoggerServiceTraceListener"/> instance. </returns>
        /// <seealso cref="LoggerServiceTraceListener"/>
        /// <seealso cref="LoggerServiceTraceListenerData"/>
        ILoggingConfigurationSendToLoggerServiceTraceListener FormatWithSharedFormatter(string formatterName);

        /// <summary>
        /// Specifies which options, or elements, should be included in messages send by this <see cref="LoggerServiceTraceListener"/>.<br/>
        /// </summary>
        /// <returns>Fluent interface that can be used to further configure the current <see cref="LoggerServiceTraceListener"/> instance. </returns>
        /// <seealso cref="LoggerServiceTraceListener"/>
        /// <seealso cref="LoggerServiceTraceListenerData"/>
        /// <seealso cref="TraceOptions"/>
        ILoggingConfigurationSendToLoggerServiceTraceListener WithTraceOptions(TraceOptions traceOptions);

        /// <summary>
        /// Specifies the <see cref="SourceLevels"/> that should be used to filter trace output by this <see cref="LoggerServiceTraceListener"/>.
        /// </summary>
        /// <returns>Fluent interface that can be used to further configure the current <see cref="LoggerServiceTraceListener"/> instance. </returns>
        /// <seealso cref="LoggerServiceTraceListener"/>
        /// <seealso cref="LoggerServiceTraceListenerData"/>
        /// <seealso cref="SourceLevels"/>
        ILoggingConfigurationSendToLoggerServiceTraceListener Filter(SourceLevels sourceLevel);

        /// <summary>
        /// Specifies the address of the endpoint that should be used when writing a log entry.
        /// </summary>
        /// <param name="writeLogEndpointAddress">The address of the endpoint that should be used when writing a log entry.</param>
        /// <returns>Fluent interface that can be used to further configure the current <see cref="LoggerServiceTraceListener"/> instance. </returns>
        /// <seealso cref="LoggerServiceTraceListener"/>
        /// <seealso cref="LoggerServiceTraceListenerData"/>
        ILoggingConfigurationSendToLoggerServiceTraceListener WithWriteLogEndpointAddress(string writeLogEndpointAddress);
    }
}
