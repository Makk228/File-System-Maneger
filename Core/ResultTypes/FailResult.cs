namespace FileSystemManager.Core.ResultTypes;

public class FailResult : IResulType
{
    public string ResultType { get; }

    public FailResult(string resultType)
    {
        ResultType = resultType;
    }
}