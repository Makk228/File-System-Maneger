namespace FileSystemManager.Core.ResultTypes;

public class SuccessResult : IResulType
{
    public string ResultType { get; }

    public SuccessResult(string resultType)
    {
        ResultType = resultType;
    }
}
