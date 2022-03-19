using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DotFramework.Infra.Security.Model
{
    public class LockUserBindingModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Lockout Reason")]
        public string LockoutReason { get; set; }

        [Required]
        [Display(Name = "Lockout By")]
        public string LockoutBy { get; set; }

        [Required]
        [Display(Name = "Lock Time")]
        public DateTime? LockTime { get; set; }
    }
}
