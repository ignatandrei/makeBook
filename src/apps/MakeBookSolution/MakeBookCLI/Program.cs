string folder = @"D:\gth\makeBook\src\structure\markdown";
WriteLine($"Folder: {folder}");
IGeneratorFiles generatorFiles = new GeneratorMarkdown(folder);
foreach (var item in generatorFiles.Validate(new ValidationContext(generatorFiles)))
{
    WriteLine("Error:" + item.ErrorMessage);
}
WriteLine($"GenerateNow: {generatorFiles.GenerateNow()}");