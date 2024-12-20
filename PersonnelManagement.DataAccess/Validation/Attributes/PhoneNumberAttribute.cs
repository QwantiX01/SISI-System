using System.Text.RegularExpressions;

namespace PersonnelManagement.DataAccess.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class PhoneNumberAttribute : Attribute
{
    public bool IsValid(object? value)
    {
        if (value is string stringValue)
            return Regex.IsMatch(stringValue, @"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");

        throw new ArgumentException("Value is not a string");
    }
}