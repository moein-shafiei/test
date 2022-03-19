using DotFramework.Core;
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace DotFramework.Infra.ExceptionHandling
{
    [DataContract]
    public class CustomExceptionDetail
    {
        public CustomExceptionDetail(Exception exception)
        {
            if (exception == null)
            {
                throw new NullReferenceException();
            }

            HelpLink = exception.HelpLink;
            Message = exception.Message;
            StackTrace = exception.StackTrace;
            Type = exception.GetType().ToString();

            if (exception is ExceptionBase)
            {
                ClassName = (exception as ExceptionBase).ClassName;
                MethodName = (exception as ExceptionBase).MethodName;
            }

            if (exception.InnerException != null)
            {
                InnerException = new CustomExceptionDetail(exception.InnerException);
            }
        }

        [DataMember]
        public string HelpLink { get; set; }

        [DataMember]
        public CustomExceptionDetail InnerException { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "An ExceptionDetail, likely created by IncludeExceptionDetailInFaults=true, whose value is:\n{1}", ToStringHelper(false));
        }

        private string ToStringHelper(bool isInner)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}", Type, Message);

            if (InnerException != null)
            {
                sb.AppendFormat(" ----> {0}", InnerException.ToStringHelper(true));
            }
            else
            {
                sb.Append("\n");
            }

            sb.Append(StackTrace);

            if (isInner)
            {
                sb.AppendFormat("\n   --- End of inner ExceptionDetail stack trace ---\n");
            }

            return sb.ToString();
        }

        public Exception Parse()
        {
            Exception exception = null;

            if (InnerException == null)
            {
                exception = Activator.CreateInstance(System.Type.GetType(Type), new object[] { Message }) as Exception;
            }
            else
            {
                exception = Activator.CreateInstance(System.Type.GetType(Type), new object[] { Message, InnerException.Parse() }) as Exception;
            }

            typeof(Exception).GetField("_stackTraceString", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField).SetValue(exception, StackTrace);

            if (exception is ExceptionBase)
            {
                (exception as ExceptionBase).ClassName = ClassName;
                (exception as ExceptionBase).MethodName = MethodName;
            }

            return exception;
        }
    }
}
