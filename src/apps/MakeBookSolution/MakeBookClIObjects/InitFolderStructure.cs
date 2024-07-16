

using System.IO.Compression;

namespace MakeBookClIObjects;
public class InitFolderStructure(string folder) : IValidatableObject
{
    public void InitNow()
    {
        bool isValid = true;
        foreach (var item in Validate(new ValidationContext(this)))
        {
            isValid = false;
            WriteLine("Error:" + item.ErrorMessage);
        }
        if (!isValid)
        {
            return;
        }
        CreateFolder(folder, ".bookSettings",it=> MarkdownInit.GetResouceBookSettings(it).ToArray(), "bookData.json");
        CreateFolder(folder, ".settings", it => MarkdownInit.GetResouceSettings(it).ToArray(), "customWord.docx", "pandocHTML.yaml", "pandocHTML.yaml.template");
        CreateFolder(folder, "book", it => MarkdownInit.GetResouceBook(it).ToArray(), "Chapter001.md", "Introduction.md", "Introduction_Assets/author.png", "Chapter001_Assets/.gitkeep");
        CreateFolder(folder, ".output", it => MarkdownInit.GetResouceOutput(it).ToArray(),  ".gitkeep");
        CreateFolder(folder, "", it => MarkdownInit.GetResouceRoot(it).ToArray(), "_readme.html");
        CreateFolder(folder, ".pandoc", it => MarkdownInit.GetResoucePandoc(it).ToArray(), "COPYING.rtf", "COPYRIGHT.txt", "MANUAL.html");
        string zip = Path.Combine(folder, ".pandoc", "pandoc.zip");
        File.WriteAllBytes(zip, MarkdownInit.GetPandocZip);
        Thread.Sleep(15000);
        ZipFile.ExtractToDirectory(zip, Path.GetDirectoryName(zip));
        Thread.Sleep(15000);
    }
    private void CreateFolder(string folderRoot, string name, Func<string, byte[]> obtainData, params string[] files)
    {
        if (!Directory.Exists(folderRoot))
        {
            Directory.CreateDirectory(folderRoot);
        }
        var folder = folderRoot;
        if (!string.IsNullOrEmpty(name)){
            folder = Path.Combine(folderRoot, name);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
        foreach (var file in files)
        {
            var fileData = obtainData(file);
            var filePath = Path.Combine(folder, file);
            var pathFile = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }
            if (!File.Exists(filePath))
            {
                File.WriteAllBytes(filePath, fileData);
            }
        }
    }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(!Directory.Exists(folder))
        {
            Exception? ex= null;
            try
            {
                Directory.CreateDirectory(folder);
            }
            catch(Exception ex1)
            {
                ex = ex1;
            }
            if (ex != null)
            {
                yield return new ValidationResult($"Error creating folder {folder} {ex.Message}", new[] { nameof(folder) });
                yield break;

            }
        }
        if (Directory.EnumerateDirectories(folder).Any())
        {
            yield return new ValidationResult($"Folder {folder} is not empty", new[] { nameof(folder) });
            yield break;
        }
        if (Directory.EnumerateFiles(folder).Any())
        {
            yield return new ValidationResult($"Folder {folder} is not empty", new[] { nameof(folder) });
            yield break;
        }
    }
}
