namespace MakeBookGenerator;

public interface IGeneratorFiles
{
    public string Folder { get;  }
    public bool GenerateNow();
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

}
