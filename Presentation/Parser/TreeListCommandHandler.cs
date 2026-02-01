using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal sealed class TreeListCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        if (parts.Length < 2 ||
            !string.Equals(parts[0], "tree", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!string.Equals(parts[1], "list", StringComparison.OrdinalIgnoreCase))
            return null;

        int depth = 1;

        if (parts.Length >= 4 &&
            string.Equals(parts[2], "-d", StringComparison.OrdinalIgnoreCase) &&
            int.TryParse(parts[3], out int parsedDepth) &&
            parsedDepth > 0)
        {
            depth = parsedDepth;
        }

        return new TreeListCommand(depth);
    }
}
