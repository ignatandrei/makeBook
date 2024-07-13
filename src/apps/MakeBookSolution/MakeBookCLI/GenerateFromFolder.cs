
using System.IO;

namespace MakeBookCLI;
internal class GenerateFromFolder
{
    private readonly IGeneratorFiles generatorFiles;
    FileSystemWatcher fileSystemWatcher;
    public GenerateFromFolder(string folder, IGeneratorFiles generator)
    {
        Folder = folder;
        this.generatorFiles = generator;
        fileSystemWatcher = new FileSystemWatcher(folder);
        fileSystemWatcher.EnableRaisingEvents = true;
        
        fileSystemWatcher.Filter = "*.*";
        fileSystemWatcher.IncludeSubdirectories = true;
        fileSystemWatcher.Created += (sender, e) => ReGenerate(e);
        fileSystemWatcher.Changed += (sender, e) => ReGenerate(e);
        fileSystemWatcher.Deleted += (sender, e) => ReGenerate(e);
        fileSystemWatcher.Renamed += (sender, e) => ReGenerate(e);
        //fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All, 1000);
    }
    public WaitForChangedResult Wait()
    {
        return fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All);
    }
    private void ReGenerate(FileSystemEventArgs e)
    {
        var whatChanged = e.FullPath;
        if(whatChanged.Contains(".output"))
        {
            return;
        }
        if(whatChanged.Contains("log.json"))
        {
            return;
        }

        WriteLine($"Regenerating for {whatChanged} at {DateTime.Now.ToString("HHmmss")}");
        GenerateNow();
    }

    public string Folder { get; }
    public void GenerateNow()
    {
        
        foreach (var item in generatorFiles.Validate(new ValidationContext(generatorFiles)))
        {
            WriteLine("Error:" + item.ErrorMessage);
        }
        WriteLine($"GenerateNow");
        var result = generatorFiles.GenerateNow();
        result.Switch(
            ok => WriteLine("Generating OK"),
            validation =>
            {
                WriteLine("There are validation problems");
                foreach (var item in validation.results)
                {
                    WriteLine(item.ToString());
                }
            },
            problems =>
            {
                var cmdsSuccess = problems.cmdsNoError();
                if (cmdsSuccess?.Length > 0)
                {
                    WriteLine("Successfully executed");
                    foreach (var item in cmdsSuccess)
                    {
                        WriteLine(item.Name);
                    }
                }
                WriteLine("Items with Problems");
                foreach (var item in problems.resultExesErrors)
                {
                    WriteLine(item.Name);
                    WriteLine(item.ToString(""));
                    WriteLine("--");
                }
            }
        );
    }
}
