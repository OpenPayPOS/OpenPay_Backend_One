using System.ComponentModel.DataAnnotations;

namespace OpenPay.Api.Models.Annotations;

public class ValidPrice : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        IConvertible valueConvertible = value as IConvertible;
        decimal d;
        if (valueConvertible == null)
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
