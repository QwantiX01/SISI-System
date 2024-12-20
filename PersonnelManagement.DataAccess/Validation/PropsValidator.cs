using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PersonnelManagement.DataAccess.Validation.Attributes;
using MaxLengthAttribute = PersonnelManagement.DataAccess.Validation.Attributes.MaxLengthAttribute;
using MinLengthAttribute = PersonnelManagement.DataAccess.Validation.Attributes.MinLengthAttribute;

namespace PersonnelManagement.DataAccess.Validation;

public class PropsValidator
{
    public static bool ValidateObject(object obj, out List<string> errors)
    {
        var type = obj.GetType();
        var props = type.GetProperties();
        errors = [];

        foreach (var prop in props)
        {
            var typeAttributes = prop.GetCustomAttributes(typeof(Attribute), true);
            foreach (var attribute in typeAttributes)
                switch (attribute)
                {
                    case MaxLengthAttribute maxLengthAttribute:
                        if (!maxLengthAttribute.IsValid(prop.GetValue(obj)))
                            errors.Add($"Property {prop.Name}: Value is longer than {maxLengthAttribute.MaxLength}");
                        break;
                    case MinLengthAttribute minLengthAttribute:
                        if (!minLengthAttribute.IsValid(prop.GetValue(obj)))
                            errors.Add(
                                $"Property {prop.Name}: Value should be greater than {minLengthAttribute.MinLength}");
                        break;
                    case RegexAttribute regexAttribute:
                        if (!regexAttribute.IsValid(prop.GetValue(obj)))
                            errors.Add($"Property {prop.Name}: Regex is invalid");
                        break;
                    case IsNotNullAttribute isNotNullAttribute:
                        if (!isNotNullAttribute.IsValid(prop.GetValue(obj)))
                            errors.Add($"Property {prop.Name}: Value is required");
                        break;
                }
        }

        return errors.Count == 0;
    }
}