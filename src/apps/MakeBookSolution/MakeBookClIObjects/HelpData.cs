using System.Diagnostics;

namespace MakeBookClIObjects;
public class HelpData
{
    public static void WriteTutorial()
    {
        var data = MyAdditionalFiles.tutorial_gen_txt;
        var tempFile = Path.GetTempFileName();
        tempFile = Path.ChangeExtension(tempFile, ".md");
        File.WriteAllText(tempFile, data);
        Console.WriteLine($"Tutorial written to {tempFile}");
        Process.Start(new ProcessStartInfo(tempFile) { UseShellExecute = true });

    }
}
