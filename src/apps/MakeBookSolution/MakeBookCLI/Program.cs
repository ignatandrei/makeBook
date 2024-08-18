//using System.Drawing.Printing;

try
{
    var latestVersion = new LatestVersion();
    var data = await latestVersion.LatestVersionNumber("ignatandrei", "MakeBook");
    
    data.Switch(
        (s) => {
            if (ThisAssembly.Info.Version.Contains(s) || s.Contains(ThisAssembly.Info.Version))
            {
                WriteLine($"You are at latest version {s}");
            }
            else
            {
                WriteLine($"You are at version !{ThisAssembly.Info.Version}! instead of !{s}!");
                WriteLine($"Please download latest version {s} from {latestVersion.LatestVersionURL}");

            }
        },
        (e) => {
            //do nothing
        }
    );
    
    if (args.Length == 0)
    {
        args = new[] { "-h" };
        //args = new[] { "--version" };

        //args = new[] { "i", "--folder", @"D:\gth\test1\" };
        //args = new[] { "gmk", "--folder", @"D:\gth\test1\" };

        //args = new[] { "gmk", "--folder", @"D:\gth\makeBook\src\help" };
        args = new[] { "t" };
    }
    RootCommand rootCommand = new();
    
    rootCommand.Description = "Generate a book from a folder";
    Option<string> folder = new
            (name: "--folder",
            description: "folder where find the book data"
            );
    folder.Arity = ArgumentArity.ExactlyOne;
    folder.AddAlias("-f");
    folder.AddAlias("-d");
    rootCommand.AddGlobalOption(folder);

    Command cmdTutorial = new("tutorial", "Tutorial about the program");
    cmdTutorial.AddAlias("t");
    cmdTutorial.SetHandler((Action)(() =>
    {
        WriteTutorial();
    }));
    rootCommand.AddCommand(cmdTutorial);

    Command cmdInit = new("init", "Initialize a folder with the book data");
    cmdInit.AddAlias("i");
    cmdInit.SetHandler((folderWithFiles) =>
    {
        
        WriteLine($"Will start init in folder: {folderWithFiles}");
        if(Directory.Exists(folderWithFiles))
        {
            Directory.Delete(folderWithFiles    , true);            
        }
        InitFolderStructure initFolder = new(folderWithFiles);
        initFolder.InitNow();
        WriteLine($"now execute gmk --folder "+folderWithFiles);
        //await Task.Delay(5000);
        //rootCommand.Invoke("gmk --folder " + folderWithFiles);

    }, folder);
    rootCommand.AddCommand(cmdInit);

    Command cmdGenerateMarkdown = new("generateFromMarkdown", "Generate from Markdown");
    cmdGenerateMarkdown.AddAlias("gmk");
    rootCommand.AddCommand(cmdGenerateMarkdown);
    
    cmdGenerateMarkdown.SetHandler((folderWithFiles) =>
    {
        string folder = folderWithFiles;
        WriteLine($"Folder: {folderWithFiles}");
        IGeneratorFiles generatorFiles = new GeneratorMarkdown(folderWithFiles);
        GenerateFromFolder generateFromFolder = new(folderWithFiles, generatorFiles);
        var result = generateFromFolder.GenerateNow();
        while (result)
        {
            Console.WriteLine("waiting for another modified file    ");
            generateFromFolder.Wait();
        }

    }, folder);
    await rootCommand.InvokeAsync(args);
    return 0;

}
catch (Exception ex)
{
    WriteLine("Exception " + ex.Message);
    return -1;
}

static void WriteTutorial()
{
    var data = MyAdditionalFiles.tutorial_gen_txt;
    var tempFile = Path.GetTempFileName();
    tempFile = Path.ChangeExtension(tempFile, ".md");
    File.WriteAllText(tempFile, data);
    Console.WriteLine($"Tutorial written to {tempFile}");
    Process.Start(new ProcessStartInfo(tempFile) { UseShellExecute = true });

}
//string folder = @"D:\gth\makeBook\src\structure\markdown";


//PrintDocument printDocument = new ();
//printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";

//// Set the output file path
//printDocument.PrinterSettings.PrintToFile = true;
//printDocument.PrinterSettings.PrintFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "output.pdf");

//try
//{
//    printDocument.Print();
//    Console.WriteLine("Document printed successfully.");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"An error occurred while printing: {ex.Message}");
//}
