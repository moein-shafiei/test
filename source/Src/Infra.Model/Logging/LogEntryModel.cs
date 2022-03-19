using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Model
{
    public class LogEntryModel
    {
        [Required]
        public Guid LogGuid { get; set; }

        public int? EventID { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public string Severity { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string MachineName { get; set; }

        [Required]
        public string AppDomainName { get; set; }

        [Required]
        public string ApplicationCode { get; set; }

        [Required]
        public string ClassName { get; set; }

        [Required]
        public string MethodName { get; set; }

        [Required]
        public string ProcessID { get; set; }

        [Required]
        public string ProcessName { get; set; }

        [Required]
        public string ThreadName { get; set; }

        [Required]
        public string Win32ThreadId { get; set; }

        public string Message { get; set; }

        [Required]
        public string FormattedMessage { get; set; }

        [Required]
        public ICollection<String> Categories { get; set; }

        [Required]
        public DateTime ModificationTime { get; set; }

        [Required]
        public Int64 SessionID { get; set; }
    }
}
