namespace PersonnelManagement.DataAccess.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class IsNotNullAttribute : Attribute
{
    public bool IsValid(object? value) => value != null;
}