using System.ComponentModel.DataAnnotations;

namespace OpenPay.Api.Models.Annotations;

public class ValidPrice : ValidationAttribute
{
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool IsValid(object value)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        decimal d;
        if (value is not IConvertible valueConvertible)
        {
            return false;
        }
        else
        {
            d = valueConvertible.ToDecimal(null);
        }

        return Decimal.Round(d, 2) == d;
    }
}
