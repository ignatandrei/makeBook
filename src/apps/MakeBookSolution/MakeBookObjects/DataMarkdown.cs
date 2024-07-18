namespace MakeBookObjects;

public class DataMarkdown(string Folder) : IValidatableObject
{
    public bool ExtractPandoc()
    {
        var fileExe = PandocExe;
        ArgumentNullException.ThrowIfNull(fileExe);
        if (File.Exists(PandocExe))
            return true;

        string zip = PandocExe.Replace(".exe", ".zip");
        if (!File.Exists(zip))
        {
            return false;
        }
        ZipFile.ExtractToDirectory(zip,Path.GetDirectoryName(fileExe)!);
        //allow time for zip file to finish
        Thread.Sleep(5000);
        return true;

    }
    private string[] GrabMarkdownDocuments()
    {
        var folder = Path.Combine(Folder, "book");
        return Directory.GetFiles(folder, "*.md");
    }
    
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
        vc = ValidateSettings().ToArray();
        if (vc.Length > 0)
        {
            return vc;
        }
        vc = ValidateBook().ToArray();
        if (vc.Length > 0)
        {
            return vc;
        }
        return Enumerable.Empty<ValidationResult>();
    }
    private IEnumerable<ValidationResult> ValidateBook()
    {
        var folder = Path.Combine(Folder, "book");
        if (!Directory.Exists(folder))
        {
            yield return new ValidationResult($"Directory {folder} does not exist");
            yield break;
        }
        var files = GrabMarkdownDocuments();
        if (files.Length == 0)
        {
            yield return new ValidationResult($"No markdown files found in {folder}");
        }

    }
    private IEnumerable<ValidationResult> ValidateSettings()
    {
        var folder = Path.Combine(Folder, ".settings");
        if (!Directory.Exists(folder))
        {
            yield return new ValidationResult($"Directory {folder} does not exist");
            yield break;
        }
        var file = Path.Combine(folder, "customWord.docx");
        if (!File.Exists(file))
        {
            yield return new ValidationResult($"File {file} does not exist", new[] { nameof(Folder) });
        }
        var fileYaml = Path.Combine(folder, "pandocHTML.yaml");
        if (!File.Exists(fileYaml))
        {
            yield return new ValidationResult($"File {fileYaml} does not exist", new[] { nameof(Folder) });
        }
        var fileTemplate = Path.Combine(folder, "pandocHTML.yaml.template");
        if (!File.Exists(fileTemplate))
        {
            yield return new ValidationResult($"File {fileTemplate} does not exist", new[] { nameof(Folder) });
            yield break;
        }
        var files= GrabMarkdownDocuments();
        var comparer = Comparer<string>.Create((a, b) =>
        {
            //Console.WriteLine("a: " + a + "==="+ a.Contains("Introduction"));
            if (a.Contains("Introduction"))
            {
                return -1;
            }
            if (b.Contains("Introduction"))
            {
                return  1;
            }
            if(a.Contains("Conclusion"))
            {
                return 1;
            }
            if (b.Contains("Conclusion"))
            {
                return -1;
            }
            return a.CompareTo(b);
        });
        //Console.WriteLine("files: " + files.Length);
        var booksRelatives=files
            .Select(it=>it.Substring(Folder.Length))
            .Select(it=>it.Replace("\\","/"))
            .Select(it =>
            {
                if(it.StartsWith("/")) return it.Substring(1);
                if (it.StartsWith(@"\")) return it.Substring(1);
                return it;
            })
            .OrderBy(x=>x,comparer)            
            .ToArray();

        //foreach (var item in booksRelatives)
        //{
        //    Console.WriteLine(item);
        //}
        var res = TemplatingData.RenderTemplating(fileTemplate, 
            new { books=files, bookRelative= booksRelatives });
        ValidationResult? var = null;
        res.Switch(
            it => {},
            it => { var = new ValidationResult($"File {it.FileName} Not found"); },
            it => { var = it[0]; }
            );
        if(var != null)
        {
           yield return var;
        }
        else
        {
           File.WriteAllText(fileYaml, res.AsT0);
        }
    }
    private IEnumerable<ValidationResult> ValidateBookSettings()
    {
        
        var bookDataPath = BookDataJson();
        bool existsBookDataPath = File.Exists(bookDataPath);
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
        if (!existsBookDataPath) { yield break; }
            string text = "";
        using (FileStream stream = File.Open(bookDataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            
            using (StreamReader reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
        }
        //Console.WriteLine("!!!text: " + text);
        this.BookData = bookData.FromJson(text);
        if (this.BookData == null)
        {
            yield return new ValidationResult($"File {bookDataPath} is not a valid json", new[] { nameof(Folder) });
        }
        else
        {
            foreach (var item in BookData.Validate(new ValidationContext(BookData)))
            {
                item.ErrorMessage = $"file {bookDataPath}:{item.ErrorMessage}";
                yield return item;
            }
            //validate locations
            var loc = this.BookData.PandocLocation();
            if(loc == null)
            {
                yield return new ValidationResult("pandoc location must exists");
                yield break;
            }
            ExtractPandoc();
            if (!File.Exists(PandocExe))
            {
                
                yield return new ValidationResult($"File {PandocExe} does not exist", new[] { nameof(Folder) });
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
        pandoc = Path.Combine(folder, "Pandoc User's Guide.html");
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
            ArgumentNullException.ThrowIfNull(BookData);
            var loc = BookData.PandocLocation();
            ArgumentNullException.ThrowIfNull(loc, nameof(loc));
            loc= Environment.ExpandEnvironmentVariables(loc);
            loc = loc.Replace('/', Path.DirectorySeparatorChar);
            loc = loc.Replace('\\', Path.DirectorySeparatorChar);
            
            if (loc.StartsWith(".pandoc"))
                return Path.Combine(Folder, loc);
            return loc;
        }
    }
    
}
