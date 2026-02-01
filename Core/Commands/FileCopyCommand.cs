using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core.Commands;

public class FileCopyCommand : FileCommandBase, ICommand
{
    public string SourcePath { get; }

    public string TargetPath { get; }

    public FileCopyCommand(string sourcePath, string targetPath)
    {
        SourcePath = sourcePath;
        TargetPath = targetPath;
    }

    public IResulType Execute(SessionContext context)
    {
        IResulType check = EnsureConnected(context);
        if (check is FailResult fail) return fail;

        string sourcePhysical = ResolvePhysicalPath(context, SourcePath);
        string targetPhysical = ResolvePhysicalPath(context, TargetPath);

        if (!context.FileSystem.Exists(sourcePhysical))
            return new FailResult($"Source does not exist: {SourcePath}");

        context.FileSystem.Copy(sourcePhysical, targetPhysical);
        return new SuccessResult($"Copied from {SourcePath} to {TargetPath}");
    }
}
