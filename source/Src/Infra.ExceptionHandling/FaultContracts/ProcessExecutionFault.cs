using System;
using System.Runtime.Serialization;

namespace DotFramework.Infra.ExceptionHandling
{
    [DataContract]
    public class ProcessExecutionFault
    {
        public ProcessExecutionFault()
        {
            ErrorRefrenceNumber = Guid.NewGuid().ToString();
        }

        [DataMember]
        public string ErrorRefrenceNumber { get; set; }

        [DataMember]
        public bool ReThrow { get; set; }

        [DataMember]
        public string EndPointName { get; set; }

        [DataMember]
        public string OperationName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public CustomExceptionDetail InnerExceptionDetail { get; set; }
    }
}
