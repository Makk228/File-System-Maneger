using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public class FileDeleteCommand : FileCommandBase, ICommand
{
    public string Path { get; }

    public FileDeleteCommand(string path)
    {
        Path = path;
    }

    public IResulType Execute(SessionContext context)
    {
        IResulType check = EnsureConnected(context);
        if (check is FailResult fail) return fail;

        string physical = ResolvePhysicalPath(context, Path);

        if (!context.FileSystem.Exists(physical))
            return new FailResult($"File or directory does not exist: {Path}");

        context.FileSystem.Delete(physical);
        return new SuccessResult($"Deleted: {Path}");
    }
}
