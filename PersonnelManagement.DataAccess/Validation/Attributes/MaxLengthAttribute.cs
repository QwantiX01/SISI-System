namespace PersonnelManagement.DataAccess.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MaxLengthAttribute : Attribute
{
    public int MaxLength { get; }

    public MaxLengthAttribute(int maxLength)
    {
        MaxLength = maxLength;
    }

    public bool IsValid(object value)
    {
        if (value is string s)
            return s.Length < MaxLength;
        throw new ArgumentException($"Value is not a string");
    }
}