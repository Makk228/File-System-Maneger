using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public interface ICommand
{
    IResulType Execute(SessionContext context);
}
