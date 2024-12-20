namespace PersonnelManagement.DataAccess.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MinLengthAttribute : Attribute
{
    public int MinLength { get; }

    public MinLengthAttribute(int minLength)
    {
        MinLength = minLength;
    }

    public bool IsValid(object? value)
    {
        if (value is string s)
            return s.Length > MinLength;

        throw new ArgumentException("Value is not a string");
    }
}