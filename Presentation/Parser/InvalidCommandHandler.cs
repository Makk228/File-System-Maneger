using FileSystemManager.Core.Commands;

namespace FileSystemManager.Presentation.Parser;

internal sealed class InvalidCommandHandler : CommandHandlerBase
{
    protected override ICommand? HandleInternal(string[] parts)
    {
        string text = string.Join(' ', parts);
        return new InvalidCommand($"Unknown command: '{text}'");
    }
}
