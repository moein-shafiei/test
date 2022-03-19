using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DotFramework.Infra.Security.Model
{
    public class AddUpdateAccountPolicyRequest
    {
        [Required]
        public String PolicyName { get; set; }
        public Int32 MaxAccountAge { get; set; }
        public Int32 MaxPasswordAge { get; set; }
        public Int32 MaxPasswordAgeReminder { get; set; }
        public Int32 EnforcePasswordHistory { get; set; }
        public Int32 AutoDeleteAccount { get; set; }
        public Int16 LockoutThresholdMethod { get; set; }
        public Int32 LockoutThreshold { get; set; }
        public Int16 LockType { get; set; }
        public Int32 LockoutDuration { get; set; }
        public Int32 RequiredLength { get; set; }
        public Boolean RequireNonLetterOrDigit { get; set; }
        public Boolean RequireDigit { get; set; }
        public Boolean RequireLowercase { get; set; }
        public Boolean RequireUppercase { get; set; }
        public Byte Status { get; set; }
    }
}
