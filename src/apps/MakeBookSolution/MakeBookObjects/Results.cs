
namespace MakeBookObjects;
public class ResultValidationProblems
{
    public readonly ValidationResult[] results;

    public ResultValidationProblems(ValidationResult[] results)
    {
        this.results = results;
    }
}
public class ResultOK
{

}
public class ResultProblemsRunExe
{
    public readonly ResultExe[] resultExes;

    public ResultProblemsRunExe(ResultExe[] resultExes)
    {
        this.resultExes = resultExes;
    }
}

[OneOf.GenerateOneOf]
public partial class Results : OneOf.OneOfBase<ResultOK, ResultValidationProblems,ResultProblemsRunExe>
{

}