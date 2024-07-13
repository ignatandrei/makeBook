
namespace MakeBookObjects;
public class ResultExe
{
    public ResultExe(DateTime startDate, ProcessStartInfo processStartInfo)
    {
        StartDate = startDate;
        Executable = processStartInfo.FileName;
        Arguments = processStartInfo.Arguments;
        WorkingDirectory = processStartInfo.WorkingDirectory;

    }
    public string? Executable { get; set; }
    public string? Arguments { get; set; }
    public int? ExitCode { get; set; }
    public string? Output { get; set; }
    public string? Error { get; set; }
    public string? WorkingDirectory { get; set; }
    public DateTime StartDate { get; internal set; }

    public static ResultExe Execute(ProcessStartInfo startInfo)
    {
        ResultExe resultExe = new (DateTime.UtcNow, startInfo);
        Process process = new Process
        {
            StartInfo = startInfo
        };
        process.Start();

        // Read the output
        resultExe.Output = process.StandardOutput.ReadToEnd();
        resultExe.Error = process.StandardError.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();
        resultExe.ExitCode = process.ExitCode;
        return resultExe;

    }
    
}

public class  ResultsExe : List<ResultExe>
{
    public ResultExe Execute(ProcessStartInfo startInfo)
    {
        var data= ResultExe.Execute(startInfo);
        Add(data);
        return data;
    }
}