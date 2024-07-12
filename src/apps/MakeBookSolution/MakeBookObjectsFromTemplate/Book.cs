
namespace MakeBookObjectsFromTemplate.SettingsJson;
public partial class Book : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(string.IsNullOrEmpty(this.Title))
            yield return new ValidationResult("Title is required", new[] { nameof(Title) });

        if(string.IsNullOrEmpty(this.Author))
            yield return new ValidationResult("Author is required", new[] { nameof(Author) });
    }
}
