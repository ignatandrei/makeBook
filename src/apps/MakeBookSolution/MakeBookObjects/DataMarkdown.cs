namespace MakeBookObjects;

public class DataMarkdown(string Folder) : IValidatableObject
{
    private bool ExtractPandoc()
    {
        var fileExe = PandocExe;
        ArgumentNullException.ThrowIfNull(fileExe);
        if (File.Exists(PandocExe))
            return true;

        string zip = PandocExe.Replace(".exe", ".zip");
        ZipFile.ExtractToDirectory(zip,Path.GetDirectoryName(fileExe)!);
        return true;

    }
    public void TryToEnsureValid()
    {
        ExtractPandoc();
    }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        TryToEnsureValid();
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
        
        var bookDataPath = BookDataJson();
        if (!File.Exists(bookDataPath))
        {
            var folderSettings = Path.GetDirectoryName(bookDataPath);
            if (!Directory.Exists(folderSettings))
            {
                yield return new ValidationResult($"Directory {folderSettings} does not exist"); ;
            }
            else
            {
                yield return new ValidationResult($"File {bookDataPath} does not exist", new[] { nameof(Folder) });

            }
        }        
        var dataBook= bookData.FromJson(File.ReadAllText(bookDataPath));
        if (dataBook == null)
        {
            yield return new ValidationResult($"File {bookDataPath} is not a valid json", new[] { nameof(Folder) });
        }
        else
        {
            foreach (var item in dataBook.Validate(new ValidationContext(dataBook)))
            {
                yield return item;
            }
        }
        
    }
    private IEnumerable<ValidationResult> ValidatePandoc()
    {
        var pandoc = PandocExe;
        ArgumentNullException.ThrowIfNullOrWhiteSpace(pandoc);
        var folder = Path.GetDirectoryName(pandoc);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(folder);
        if (!File.Exists(pandoc))
        {            
            if (!Directory.Exists(folder))
            {
                yield return new ValidationResult($"Directory {folder} does not exists");
            }
            else
            {
                yield return new ValidationResult($"File {pandoc} does not exist", new[] { nameof(Folder) });
            }
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
    public MakeBookObjectsFromTemplate.SettingsJson.bookData? BookData;
    private string BookDataJson()
    {
        var folder = Path.Combine(Folder, ".booksettings");
        return Path.Combine(folder, "bookdata.json");

    }
    public string PandocExe
    {
        get
        {
            var folder = Path.Combine(Folder, ".pandoc");
            return Path.Combine(folder, "pandoc.exe");
        }
    }
    
}
