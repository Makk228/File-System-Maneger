using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public class ConnectCommand : ICommand
{
    public string AbsolutPath { get; }

    public ConnectCommand(string absolutPath)
    {
        AbsolutPath = absolutPath;
    }

    public IResulType Execute(SessionContext context)
    {
        return context.Connect(AbsolutPath);
    }
}
