namespace MakeBookObjects;
public interface IGeneratorFiles
{
    public string Folder { get; }
    public Results GenerateNow();
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
}
