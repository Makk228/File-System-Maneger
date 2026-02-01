using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public class TreeGotoCommand : ICommand
{
    public string Path { get; }

    public TreeGotoCommand(string path)
    {
        Path = path;
    }

    public IResulType Execute(SessionContext context)
    {
        return context.ChangeDirectory(Path);
    }
}
