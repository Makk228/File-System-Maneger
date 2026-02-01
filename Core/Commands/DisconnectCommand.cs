using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public class DisconnectCommand : ICommand
{
    public IResulType Execute(SessionContext context)
    {
        return context.Disconnect();
    }
}
