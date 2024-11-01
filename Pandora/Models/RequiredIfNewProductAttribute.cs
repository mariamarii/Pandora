using System.ComponentModel.DataAnnotations;

public class RequiredIfNewProductAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;
    private readonly object _expectedValue;

    public RequiredIfNewProductAttribute(string dependentProperty, object expectedValue)
    {
        _dependentProperty = dependentProperty;
        _expectedValue = expectedValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dependentProperty = validationContext.ObjectType.GetProperty(_dependentProperty);
        if (dependentProperty == null)
        {
            return new ValidationResult($"Unknown property: {_dependentProperty}");
        }

        var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance, null);

        if (dependentValue.Equals(_expectedValue) && value == null)
        {
            return new ValidationResult(ErrorMessage ?? "Field is required.");
        }

        return ValidationResult.Success;
    }
}
