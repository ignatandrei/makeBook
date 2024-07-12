
namespace MakeBookObjectsFromTemplate.SettingsJson;

public partial class Commands: IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(string.IsNullOrWhiteSpace(this.Name))
            yield return new ValidationResult("Name is required", new[] { nameof(Name) });
        if (string.IsNullOrWhiteSpace(this.Value))
            yield return new ValidationResult("Value is required", new[] { nameof(Value) });
    }
}