//using System.Drawing.Printing;

try
{
    if (args.Length == 0)
    {
        args = new[] { "-h" };
        args = new[] { "gmk","--folder", @"D:\gth\makeBook\src\structure\markdown\" };
        //args = new[] { "gmk", "--folder", @"D:\gth\makeBook\src\" };
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
