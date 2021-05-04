using System.ComponentModel.DataAnnotations;
using MyStagram.Core.Extensions;

namespace MyStagram.Core.Validators
{
    public class WhitespacesNotAllowedValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string val = (string)value;

            if(val.HasWhitespaces() && !string.IsNullOrEmpty(val))
                return new ValidationResult("Whitespaces are not allowed");

            return ValidationResult.Success;
        }
    }
}