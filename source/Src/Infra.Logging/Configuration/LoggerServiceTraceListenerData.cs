using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System.Configuration;
using System.Diagnostics;

namespace DotFramework.Infra.Logging.Configuration
{
    /// <summary>
    /// Configuration object for a <see cref="LoggerServiceTraceListener"/>.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "LoggerServiceTraceListenerDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "LoggerServiceTraceListenerDataDisplayName")]
    public class LoggerServiceTraceListenerData : TraceListenerData
    {
        private const string _FormatterNameProperty = "formatter";
        private const string _WriteLogEndpointAddressProperty = "writeLogEndpointAddress";

        /// <summary>
        /// Initializes a <see cref="LoggerServiceTraceListenerData"/>.
        /// </summary>
        public LoggerServiceTraceListenerData()
            : base(typeof(LoggerServiceTraceListener))
        {
            ListenerDataType = typeof(LoggerServiceTraceListenerData);
        }

        /// <summary>
        /// Initializes a named instance of <see cref="LoggerServiceTraceListenerData"/> with 
        /// name, endpoint address, and formatter name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="writeLogEndpointAddress">The endpoint address for writing the log.</param>
        /// <param name="formatterName">The formatter name.</param>        
        public LoggerServiceTraceListenerData(string name,
                                          string writeLogEndpointAddress,
                                          string formatterName)
            : this(
                name,
                writeLogEndpointAddress,
                formatterName,
                TraceOptions.None,
                SourceLevels.All)
        {
        }

        /// <summary>
        /// Initializes a named instance of <see cref="LoggerServiceTraceListenerData"/> with 
        /// name, endpoint address, and formatter name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="writeLogEndpointAddress">The endpoint address for writing the log.</param>
        /// <param name="formatterName">The formatter name.</param>
        /// <param name="traceOutputOptions">The trace options.</param>
        /// <param name="filter">The filter to be applied</param>
        public LoggerServiceTraceListenerData(string name,
                                          string writeLogEndpointAddress,
                                          string formatterName,
                                          TraceOptions traceOutputOptions,
                                          SourceLevels filter)
            : base(name, typeof(LoggerServiceTraceListener), traceOutputOptions, filter)
        {
            WriteLogEndpointAddress = writeLogEndpointAddress;
            Formatter = formatterName;
        }

        /// <summary>
        /// Gets and sets the endpoint address for writing the log.
        /// </summary>
        [ConfigurationProperty(_WriteLogEndpointAddressProperty, IsRequired = true, DefaultValue = "WriteLog")]
        [ResourceDescription(typeof(DesignResources), "LoggerServiceTraceListenerWriteLogEndpointAddressDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggerServiceTraceListenerWriteLogEndpointAddressDisplayName")]
        public string WriteLogEndpointAddress
        {
            get { return (string)base[_WriteLogEndpointAddressProperty]; }
            set { base[_WriteLogEndpointAddressProperty] = value; }
        }

        /// <summary>
        /// Gets and sets the formatter name.
        /// </summary>
        [ConfigurationProperty(_FormatterNameProperty, IsRequired = false)]
        [ResourceDescription(typeof(DesignResources), "LoggerServiceTraceListenerDataFormatterDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggerServiceTraceListenerDataFormatterDisplayName")]
        [Reference(typeof(NameTypeConfigurationElementCollection<FormatterData, CustomFormatterData>), typeof(FormatterData))]
        public string Formatter
        {
            get { return (string)base[_FormatterNameProperty]; }
            set { base[_FormatterNameProperty] = value; }
        }

        /// <summary>
        /// Builds the <see cref="TraceListener" /> object represented by this configuration object.
        /// </summary>
        /// <param name="settings">The configuration settings for logging.</param>
        /// <returns>
        /// A trace listener.
        /// </returns>
        protected override TraceListener CoreBuildTraceListener(LoggingSettings settings)
        {
            LoggerServiceManager.Instance.Initialize(WriteLogEndpointAddress);

            var formatter = BuildFormatterSafe(settings, Formatter);
            return new LoggerServiceTraceListener(formatter);
        }
    }
}
