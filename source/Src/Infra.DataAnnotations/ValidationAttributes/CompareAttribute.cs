using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Resources;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CompareAttribute : ValidationAttribute
    {
        public string OtherProperty
        {

            get;
            private set;
        }

        public string OtherPropertyDisplayName
        {

            get;
            internal set;
        }

        public CompareAttribute(string otherProperty)
            : base("'{0}' and '{1}' do not match.")
        {
            if (otherProperty == null)
            {
                throw new ArgumentNullException("otherProperty");
            }
            this.OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[]
			{
				name,
				this.OtherPropertyDisplayName ?? this.OtherProperty
			});
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(this.OtherProperty);

            if (property == null)
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Could not find a property named {0}.", new object[]
				{
					this.OtherProperty
				}));
            }

            object value2 = property.GetValue(validationContext.ObjectInstance, null);

            if (!object.Equals(value, value2))
            {
                if (this.OtherPropertyDisplayName == null)
                {
                    this.OtherPropertyDisplayName = CompareAttribute.GetDisplayNameForProperty(validationContext.ObjectType, this.OtherProperty);
                }
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }

        private static string GetDisplayNameForProperty(Type containerType, string propertyName)
        {
            var property  = containerType.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyName);

            if (property == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The property {0}.{1} could not be found.", new object[]
				{
					containerType.FullName,
					propertyName
				}));
            }

            DisplayAttribute displayAttribute = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayAttribute != null)
            {
                return displayAttribute.GetName();
            }

            DisplayNameAttribute displayNameAttribute = (DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
            
            if (displayNameAttribute != null)
            {
                return displayNameAttribute.DisplayName;
            }

            return propertyName;
        }
    }
}