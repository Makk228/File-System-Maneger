using FileSystemManager.Core.ResultTypes;
using System;

namespace FileSystemManager.Core.Commands;

public class FileRenameCommand : FileCommandBase, ICommand
{
    public string Path { get; }

    public string Name { get; }

    public FileRenameCommand(string path, string name)
    {
        Path = path;
        Name = name;
    }

    public IResulType Execute(SessionContext context)
    {
        IResulType check = EnsureConnected(context);
        if (check is FailResult fail) return fail;

        if (Name.Contains('/', StringComparison.Ordinal) ||
            Name.Contains('\\', StringComparison.Ordinal))
        {
            return new FailResult("Name must not contain path separators.");
        }

        string physical = ResolvePhysicalPath(context, Path);

        if (!context.FileSystem.Exists(physical))
            return new FailResult($"File or directory does not exist: {Path}");

        context.FileSystem.Rename(physical, Name);

        return new SuccessResult($"Renamed {Path} to {Name}");
    }
}
