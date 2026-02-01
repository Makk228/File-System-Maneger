using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class DisconnectCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        return parts.Length == 0 ||
            !string.Equals(parts[0], "disconnect", StringComparison.OrdinalIgnoreCase)
            ? null
            : (ICommand)new DisconnectCommand();
    }
}
