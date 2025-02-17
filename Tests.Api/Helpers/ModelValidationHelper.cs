using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenPay.Tests.Api.Helpers;
public static class ModelValidationHelper
{
    public static IList<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();

        var validationContext = new ValidationContext(model);

        Validator.TryValidateObject(model, validationContext, results, true);

        if (model is IValidatableObject validatableObject)
            results.AddRange(validatableObject.Validate(validationContext));

        return results;
    }
}