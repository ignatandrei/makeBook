
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
    public readonly ResultExe[] resultExesErrors;
    private readonly Commands[] cmds;

    public ResultProblemsRunExe(Commands[] cmds, ResultExe[] resultExes)
    {
        this.resultExesErrors = resultExes.ToArray();
        this.cmds = cmds;
    }
    public Commands[] cmdsNoError()
    {
        return cmds.Where(x => resultExesErrors.All(y => y.Name != x.Name)).ToArray();
    }
    public Commands[] cmdsError()
    {
        return cmds.Where(x => resultExesErrors.Any(y => y.Name == x.Name)).ToArray();
    }
}

[OneOf.GenerateOneOf]
public partial class Results : OneOf.OneOfBase<ResultOK, ResultValidationProblems,ResultProblemsRunExe>
{

}