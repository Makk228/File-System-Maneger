using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class ConnectCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        if (parts.Length == 0 ||
            !string.Equals(parts[0], "connect", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (parts.Length < 2)
            return new InvalidCommand("connect requires path argument.");

        string path = parts[1];
        return new ConnectCommand(path);
    }
}
