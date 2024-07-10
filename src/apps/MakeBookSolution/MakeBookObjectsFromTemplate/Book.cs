
namespace MakeBookObjectsFromTemplate.SettingsJson;

public partial class bookData: IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Book == null)
        {
            yield return new ValidationResult("Book is required", new[] { nameof(Book) });

        }
        else
        {
            foreach(var item in this.Book!.Validate(validationContext))
            {
                yield return item;
            }
        }
    }
    public static bookData? FromJson(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<bookData>(json);
    }
}
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
