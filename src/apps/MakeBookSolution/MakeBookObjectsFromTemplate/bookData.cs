
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
        if(this.Locations == null || this.Locations.Count() == 0)
        {
            yield return new ValidationResult("Locations is required", new[] { nameof(Commands) });
        }
        else
        {
            if (PandocLocation() == null)
                yield return new ValidationResult("should have in locations pandoc ", new[] { nameof(Locations) });
        }
    }
    public string? PandocLocation()
    {
        if (this.Locations == null) return null;
        return this.Locations
            .Where(it => it.Name == "pandoc")
            .Select(it => it?.Value)
            .FirstOrDefault()
            ;
    }
    public static bookData? FromJson(string json)
    {
        var data= System.Text.Json.JsonSerializer.Deserialize<bookData>(json);
        if(data == null)
        {
            return null;
        }
        if(data.Commands == null)
        {
            return data;
        }
        if (data.Book == null)
            return data;
        foreach (var cmd in data.Commands)
        {
            //TODO 2025-01-01 make RSCG_IFormattable for parsing commands
            if (!string.IsNullOrEmpty(cmd.Value))
            {
                cmd.Value = cmd.Value.Replace("{title}", data.Book.Title).Replace("{author}", data.Book.Author);
            }
        }
        return data;
    }
}
