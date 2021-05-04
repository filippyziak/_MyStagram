using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace MyStagram.Core.Validators
{
    public class FileExtensionsValidator : ValidationAttribute
    {
        private readonly string[] extensions;
        public bool IsCollection { get; set; }
        public FileExtensionsValidator(params string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;
            
            if(!IsCollection)
            {
                if(!IsValidExtension(value as IFormFile))
                    return new ValidationResult(GetErrorMessage());
            }
            else
            {
                var files = value as List<IFormFile>;

                foreach (var file in files)
                    if(!IsValidExtension(file))
                        return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private bool IsValidExtension(IFormFile file) => extensions.Any(e => e == Path.GetExtension(file.FileName.ToLower()));

        private string GetErrorMessage()
        {
            var errorMessage = $"Allowed file extensions are: ";

            for (int i = 0; i< extensions.Length; i++)
                    errorMessage += i != extensions.Length - 1 ?$"{extensions[i]}, " : errorMessage += extensions[i];

            return errorMessage;
        }
    }
}