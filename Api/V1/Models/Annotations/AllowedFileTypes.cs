using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OpenPay.Api.V1.Models.Annotations;
public class AllowedFileTypes : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedFileTypes(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(ErrorMessage ?? "This file type is not allowed");
            }
        }

        return ValidationResult.Success;
    }
}