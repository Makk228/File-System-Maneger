using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class FileRenameCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        if (parts.Length < 2 ||
            !string.Equals(parts[0], "file", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!string.Equals(parts[1], "rename", StringComparison.OrdinalIgnoreCase))
            return null;

        if (parts.Length < 4)
            return new InvalidCommand("file rename requires path and new name.");

        return new FileRenameCommand(parts[2], parts[3]);
    }
}
