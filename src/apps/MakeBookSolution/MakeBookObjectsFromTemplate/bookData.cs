
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
        if(this.Commands == null)
        {
            yield return new ValidationResult("Commands is required", new[] { nameof(Commands) });
        }
        else
        {
            foreach(var item in this.Commands)
            {
                foreach(var valCommand in item.Validate(validationContext))
                {
                    yield return valCommand;
                }
            }
        }
    }
    public static bookData? FromJson(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<bookData>(json);
    }
}
