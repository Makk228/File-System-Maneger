using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class FileDeleteCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        if (parts.Length < 2 ||
            !string.Equals(parts[0], "file", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!string.Equals(parts[1], "delete", StringComparison.OrdinalIgnoreCase))
            return null;

        if (parts.Length < 3)
            return new InvalidCommand("file delete requires path argument.");

        return new FileDeleteCommand(parts[2]);
    }
}
