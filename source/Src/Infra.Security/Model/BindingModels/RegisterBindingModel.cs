using System;
using System.ComponentModel.DataAnnotations;

namespace DotFramework.Infra.Security.Model
{
    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string _FullName;
        [Required]
        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(_FullName))
                {
                    return $"{FirstName} {LastName}";
                }
                else
                {
                    return _FullName;
                }
            }
            set
            {
                _FullName = value;
            }
        }

        [Display(Name = "Account Policy")]
        public string AccountPolicy { get; set; }

        [Display(Name = "Initial Role")]
        public string InitialRole { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}