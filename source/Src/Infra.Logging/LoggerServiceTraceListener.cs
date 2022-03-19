using DotFramework.Infra.Logging.Configuration;
using DotFramework.Infra.Model;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System;
using System.Diagnostics;

namespace DotFramework.Infra.Logging
{
    /// <summary>
    /// A <see cref="System.Diagnostics.TraceListener"/> that writes to a database, formatting the output with an <see cref="ILogFormatter"/>.
    /// </summary>
    [ConfigurationElementType(typeof(LoggerServiceTraceListenerData))]
    public class LoggerServiceTraceListener : FormattedTraceListenerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LoggerServiceTraceListener"/>.
        /// </summary>
        /// <param name="formatter">The formatter.</param>        
        public LoggerServiceTraceListener(
            ILogFormatter formatter)
            : base(formatter)
        {

        }

        /// <summary>
        /// The Write method 
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Write(string message)
        {
            //ExecuteWriteLogStoredProcedure(0, 5, TraceEventType.Information, string.Empty, DateTime.Now, string.Empty,
            //                               string.Empty, string.Empty, string.Empty, null, null, message);
        }

        public override void Write(string message, string category)
        {
            base.Write(message, category);
        }

        public override void Write(object o)
        {
            base.Write(o);
        }

        public override void Write(object o, string category)
        {
            base.Write(o, category);
        }

        /// <summary>
        /// The WriteLine method.
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void WriteLine(string message)
        {
            Write(message);
        }

        public override void WriteLine(string message, string category)
        {
            base.WriteLine(message, category);
        }

        public override void WriteLine(object o)
        {
            base.WriteLine(o);
        }

        public override void WriteLine(object o, string category)
        {
            base.WriteLine(o, category);
        }

        /// <summary>
        /// Delivers the trace data to the underlying database.
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
        /// <param name="source">The name of the trace source that delivered the trace data.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="id">The id of the event.</param>
        /// <param name="data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    LogEntry logEntry = data as LogEntry;

                    if (ValidateParameters(logEntry))
                    {
                        LoggerServiceManager.Instance.WriteLog(GetLogEntryModel(logEntry));
                    }
                }
                else if (data is string)
                {
                    Write(data as string);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }



        /// <summary>
        /// Declares the supported attributes for <see cref="LoggerServiceTraceListener"/>.
        /// </summary>
        protected override string[] GetSupportedAttributes()
        {
            return new string[1] { "formatter" };
        }

        /// <summary>
        /// Validates that enough information exists to attempt executing the stored procedures
        /// </summary>
        /// <param name="logEntry">The LogEntry to validate.</param>
        /// <returns>A Boolean indicating whether the parameters for the LogEntry configuration are valid.</returns>
        private bool ValidateParameters(LogEntry logEntry)
        {
            bool valid = true;
            return valid;
        }

        /// <summary>
        /// Parse A <see cref="LogEntry"/> to <see cref="LogEntryModel"/>
        /// </summary>
        /// <param name="logEntry">Input <see cref="LogEntry"/></param>
        /// <returns>Parsed <see cref="LogEntry"/></returns>
        private LogEntryModel GetLogEntryModel(LogEntry logEntry)
        {
            LogEntryModel model = new LogEntryModel();

            if (logEntry.ExtendedProperties.ContainsKey("Trace ID"))
            {
                model.LogGuid = new Guid(logEntry.ExtendedProperties["Trace ID"].ToString());
            }
            else
            {
                model.LogGuid = Guid.NewGuid();
            }

            model.EventID = logEntry.EventId;
            model.Priority = logEntry.Priority;
            model.Severity = logEntry.Severity.ToString();
            model.Title = logEntry.Title;
            model.Timestamp = logEntry.TimeStamp;
            model.MachineName = logEntry.MachineName;
            model.AppDomainName = logEntry.AppDomainName;

            if (logEntry.ExtendedProperties.ContainsKey("Application Code"))
            {
                string applicationCode = logEntry.ExtendedProperties["Application Code"]?.ToString() ?? "Undefined";

                logEntry.ExtendedProperties["Application Code"] = applicationCode;
                model.ApplicationCode = applicationCode;
            }

            if (logEntry.ExtendedProperties.ContainsKey("Class Name"))
            {
                model.ClassName = logEntry.ExtendedProperties["Class Name"].ToString();
            }
            else
            {
                model.ClassName = "Undefined";
            }

            if (logEntry.ExtendedProperties.ContainsKey("Method Name"))
            {
                model.MethodName = logEntry.ExtendedProperties["Method Name"].ToString();
            }
            else
            {
                model.MethodName = "Undefined";
            }

            model.ProcessID = logEntry.ProcessId;
            model.ProcessName = logEntry.ProcessName;
            model.ThreadName = logEntry.ManagedThreadName;
            model.Win32ThreadId = logEntry.Win32ThreadId;
            model.Message = logEntry.Message;

            if (Formatter != null)
            {
                model.FormattedMessage = Formatter.Format(logEntry);
            }

            model.Categories = logEntry.Categories;
            model.ModificationTime = DateTime.Now;
            model.SessionID = 0;

            return model;
        }
    }
}
