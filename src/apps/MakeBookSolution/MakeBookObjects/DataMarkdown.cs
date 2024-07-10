namespace MakeBookObjects;

public class DataMarkdown(string Folder) : IValidatableObject
{

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        var vc = ValidateBookSettings().ToArray();
        if (vc.Length > 0)
        {
            return vc;
        }
        vc = ValidatePandoc().ToArray();
        if (vc.Length > 0)
        {
            return vc;
        }
        return Enumerable.Empty<ValidationResult>();
    }

    private IEnumerable<ValidationResult> ValidateBookSettings()
    {
        var folder = Path.Combine(Folder, ".booksettings");
        if (!Directory.Exists(folder))
        {
            yield return new ValidationResult($"Directory {folder} does not exist", new[] { nameof(Folder) });
        }
        var bookData = Path.Combine(folder, "bookdata.json");
        if (!File.Exists(bookData))
        {
            yield return new ValidationResult($"File {bookData} does not exist", new[] { nameof(Folder) });
        }
    }
    private IEnumerable<ValidationResult> ValidatePandoc()
    {
        var folder = Path.Combine(Folder, ".pandoc");
        if (!Directory.Exists(folder))
        {
            yield return new ValidationResult($"Directory {folder} does not exist", new[] { nameof(Folder) });
        }
        var pandoc = Path.Combine(folder, "pandoc.exe");
        if (!File.Exists(pandoc))
        {
            yield return new ValidationResult($"File {pandoc} does not exist", new[] { nameof(Folder) });
        }
        pandoc = Path.Combine(folder, "COPYING.rtf");
        if (!File.Exists(pandoc))
        {
            yield return new ValidationResult($"File {pandoc} does not exist", new[] { nameof(Folder) });
        }
        pandoc = Path.Combine(folder, "COPYRIGHT.txt");
        if (!File.Exists(pandoc))
        {
            yield return new ValidationResult($"File {pandoc} does not exist", new[] { nameof(Folder) });
        }
        pandoc = Path.Combine(folder, "MANUAL.html");
        if (!File.Exists(pandoc))
        {
            yield return new ValidationResult($"File {pandoc} does not exist", new[] { nameof(Folder) });
        }
    }

    public string PandocExe
    {
        get
        {
            return Path.Combine(Folder, ".pandoc", "pandoc.exe");
        }
    }
}
