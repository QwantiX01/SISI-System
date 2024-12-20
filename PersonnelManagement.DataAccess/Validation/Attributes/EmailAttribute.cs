using System.Text.RegularExpressions;

namespace PersonnelManagement.DataAccess.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class EmailAttribute : Attribute
{
    public bool IsValid(object? value)
    {
        if (value is string stringValue)
            return Regex.IsMatch(stringValue, @"^[\w-\.]+@([\w-]+\.)+[\w-]{{2,4}}$");

        throw new ArgumentException("Value is not a string");
    }
}