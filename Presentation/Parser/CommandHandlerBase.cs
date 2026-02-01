using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

internal abstract class CommandHandlerBase : IHandler
{
    private IHandler? _nextHandler;

    public void SetNext(IHandler nextHandler)
    {
        _nextHandler = nextHandler ?? throw new ArgumentNullException(nameof(nextHandler));
    }

    public ICommand? Handle(string[] parts)
    {
        ArgumentNullException.ThrowIfNull(parts);

        ICommand? resultType = HandleInternal(parts);
        if (resultType is not null)
            return resultType;

        return _nextHandler?.Handle(parts);
    }

    protected abstract ICommand? HandleInternal(string[] parts);
}
