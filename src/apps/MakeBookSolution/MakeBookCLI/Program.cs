//using System.Drawing.Printing;

string folder = @"D:\gth\makeBook\src\structure\markdown";
WriteLine($"Folder: {folder}");


IGeneratorFiles generatorFiles = new GeneratorMarkdown(folder);
GenerateFromFolder generateFromFolder = new (folder, generatorFiles);
generateFromFolder.GenerateNow();
while (true)
{
    Console.WriteLine("wait");
    generateFromFolder.Wait();
}


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
