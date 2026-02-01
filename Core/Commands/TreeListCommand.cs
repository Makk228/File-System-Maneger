using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.ResultTypes;
using System.Text;

namespace FileSystemManager.Core.Commands;

public class TreeListCommand : ICommand
{
    public int Depth { get; }

    public TreeListCommand(int depth)
    {
        Depth = depth;
    }

    public IResulType Execute(SessionContext context)
    {
        if (!context.IsConnected || context.CurrentDirectory is null)
            return new FailResult("Not connected. Please connect first.");

        var builder = new StringBuilder();

        builder.AppendLine(context.LocalPath ?? "/");

        PrintDirectory(context.CurrentDirectory, currentDepth: 1, builder);

        return new SuccessResult(builder.ToString());
    }

    private void PrintDirectory(IDirectory directory, int currentDepth, StringBuilder builder)
    {
        if (currentDepth > Depth)
            return;

        foreach (INode node in directory.GetChildren())
        {
            builder.Append(' ', currentDepth * 2);

            bool isDirectory = node is IDirectory;

            builder.Append(isDirectory ? "[D] " : "[F] ");
            builder.AppendLine(node.Name);

            if (isDirectory)
                PrintDirectory((IDirectory)node, currentDepth + 1, builder);
        }
    }
}
