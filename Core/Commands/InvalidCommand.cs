using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public sealed class InvalidCommand : ICommand
{
    private readonly string _message;

    public InvalidCommand(string message)
    {
        _message = message;
    }

    public IResulType Execute(SessionContext context)
    {
        return new FailResult(_message);
    }
}
