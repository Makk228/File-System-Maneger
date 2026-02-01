using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class TreeGotoCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        if (parts.Length < 2 ||
            !string.Equals(parts[0], "tree", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!string.Equals(parts[1], "goto", StringComparison.OrdinalIgnoreCase))
            return null;

        if (parts.Length < 3)
            return new InvalidCommand("tree goto requires path argument.");

        string path = parts[2];
        return new TreeGotoCommand(path);
    }
}
