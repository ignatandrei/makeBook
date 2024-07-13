//using System.Drawing.Printing;

string folder = @"D:\gth\makeBook\src\structure\markdown";
WriteLine($"Folder: {folder}");
IGeneratorFiles generatorFiles = new GeneratorMarkdown(folder);
foreach (var item in generatorFiles.Validate(new ValidationContext(generatorFiles)))
{
    WriteLine("Error:" + item.ErrorMessage);
}
WriteLine($"GenerateNow: {generatorFiles.GenerateNow()}");
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
