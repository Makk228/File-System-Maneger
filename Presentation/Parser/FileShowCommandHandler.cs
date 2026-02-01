using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class FileShowCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        if (parts.Length < 2 ||
            !string.Equals(parts[0], "file", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!string.Equals(parts[1], "show", StringComparison.OrdinalIgnoreCase))
            return null;

        if (parts.Length < 3)
            return new InvalidCommand("file show requires path argument.");

        return new FileShowCommand(parts[2]);
    }
}
