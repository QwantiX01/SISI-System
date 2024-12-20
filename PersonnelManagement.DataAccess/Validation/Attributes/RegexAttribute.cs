using System.Text.RegularExpressions;

namespace PersonnelManagement.DataAccess.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RegexAttribute : Attribute
{
    public string RegexPattern { get; }

    public RegexAttribute(string regexPattern)
    {
        RegexPattern = regexPattern;
    }

    public bool IsValid(object value)
    {
        if (value is string s)
            return Regex.IsMatch(s, RegexPattern);
        throw new ArgumentException("Value is not a string");
    }
}