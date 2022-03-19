using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Security.Model
{
    public class LockInfoResponseModel
    {
        public DateTimeOffset LockoutEndDateUtc { get; set; }

        public DateTime? LastLockTime { get; set; }

        public Boolean? LockoutEnabled { get; set; }

        public String LockoutReason { get; set; }

        public Byte LockoutType { get; set; }

        public String LockoutBy { get; set; }
    }
}
