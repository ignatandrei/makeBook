

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
    public ResultsExe resultExes { get; } = new();
    public Commands[]? cmds; 
    public Results GenerateNow()
    {
        data.TryToEnsureValid();
        var problems = data.Validate(new ValidationContext(this)).ToArray();
        if (problems.Any())
        {
            return new ResultValidationProblems(problems);
        }
        
        ArgumentNullException.ThrowIfNull(data.BookData);
        var commands = data.BookData.Commands?.ToArray();
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
            ResultExe res= this.resultExes.Execute(startInfo);
            res.Name = cmd.Name;
            // Create and start the process 
            continue;
        }
        var errors = resultExes.Where(x => x.ExitCode != 0).ToArray();  
        if(errors.Length==0)
        {
            return new ResultOK();
        }
        
        return new ResultProblemsRunExe(commands,errors);
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return data.Validate(validationContext);
    }
}
