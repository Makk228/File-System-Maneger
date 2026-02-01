using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public class FileShowCommand : FileCommandBase, ICommand
{
    public string Path { get; }

    public FileShowCommand(string path)
    {
        Path = path;
    }

    public IResulType Execute(SessionContext context)
    {
        IResulType check = EnsureConnected(context);
        if (check is FailResult fail) return fail;

        string physicalPath = ResolvePhysicalPath(context, Path);

        if (!context.FileSystem.FileExists(physicalPath))
            return new FailResult($"File not found: {Path}");

        string content = context.FileSystem.ReadFile(physicalPath);
        return new SuccessResult(content);
    }
}
