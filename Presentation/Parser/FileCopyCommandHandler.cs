using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class FileCopyCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        if (parts.Length < 2 ||
            !string.Equals(parts[0], "file", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!string.Equals(parts[1], "copy", StringComparison.OrdinalIgnoreCase))
            return null;

        if (parts.Length < 4)
            return new InvalidCommand("file copy requires source and target.");

        return new FileCopyCommand(parts[2], parts[3]);
    }
}
