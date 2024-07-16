//using System.Drawing.Printing;
    try
{
    if (args.Length == 0)
    {
        args = new[] { "-h" };
        //args = new[] { "--version" };

        //args = new[] { "i", "--folder", @"D:\gth\test1\" };
        //args = new[] { "gmk", "--folder", @"D:\gth\test1\" };

        args = new[] { "i", "--folder", @"D:\gth\makeBook\src\help" };
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
