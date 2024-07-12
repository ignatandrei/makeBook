

namespace MakeBookObjects;
public class GeneratorMarkdown : IGeneratorFiles
{
    private DataMarkdown data { get; }
    public GeneratorMarkdown(string folder)
    {
        Folder = folder;
        data = new DataMarkdown(folder);
    }
    public string Folder { get; }

    public bool GenerateNow()
    {
        data.TryToEnsureValid();
        if(data.Validate(new ValidationContext(this)).Any())
        {
            return false;
        }
        ArgumentNullException.ThrowIfNull(data.BookData);
        var commands = data.BookData.Commands;
        ArgumentNullException.ThrowIfNull(commands);
        foreach (var cmd in commands)
        {

            Console.WriteLine($"Running: {cmd.Name}");
            var pandocExe = data.PandocExe;
            ProcessStartInfo startInfo = new()
            {
                FileName = pandocExe,
                WorkingDirectory = Folder,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false,
                //Arguments = "-d .settings/pandocHTML.yaml -o .output/index.docx"
                //Arguments = "-d .settings/pandocHTML.yaml -o .output/index.md -t gfm"
                Arguments = cmd.Value
            };

            // Create and start the process
            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();

            // Read the output
            string output = process.StandardOutput.ReadToEnd();
            string errorOutput = process.StandardError.ReadToEnd();

            // Wait for the process to exit
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                Console.WriteLine($"Error: {errorOutput}");
                continue;
            }
            continue;
        }
        return true;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return data.Validate(validationContext);
    }
}
