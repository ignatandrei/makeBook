

namespace MakeBookClIObjects;
public class InitFolderStructure(string folder) : IValidatableObject
{
    public void InitNow()
    {
        bool isValid = true;
        foreach (var item in Validate(new ValidationContext(this)))
        {
            isValid = false;
            WriteLine("Error:" + item.ErrorMessage);
        }
        if (!isValid)
        {
            return;
        }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(!Directory.Exists(folder))
        {
            Exception? ex= null;
            try
            {
                Directory.CreateDirectory(folder);
            }
            catch(Exception ex1)
            {
                ex = ex1;
            }
            if (ex != null)
            {
                yield return new ValidationResult($"Error creating folder {folder} {ex.Message}", new[] { nameof(folder) });
                yield break;

            }
        }
        if (Directory.EnumerateDirectories(folder).Any())
        {
            yield return new ValidationResult($"Folder {folder} is not empty", new[] { nameof(folder) });
            yield break;
        }
        if (Directory.EnumerateFiles(folder).Any())
        {
            yield return new ValidationResult($"Folder {folder} is not empty", new[] { nameof(folder) });
            yield break;
        }
    }
}
