//using System.Drawing.Printing;

string folder = @"D:\gth\makeBook\src\structure\markdown";
WriteLine($"Folder: {folder}");
IGeneratorFiles generatorFiles = new GeneratorMarkdown(folder);
foreach (var item in generatorFiles.Validate(new ValidationContext(generatorFiles)))
{
    WriteLine("Error:" + item.ErrorMessage);
}
WriteLine($"GenerateNow");
var result =  generatorFiles.GenerateNow() ;
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
        if(cmdsSuccess?.Length > 0)
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
