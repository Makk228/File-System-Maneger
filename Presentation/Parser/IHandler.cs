using FileSystemManager.Core.Commands;

namespace FileSystemManager.Presentation.Parser;

internal interface IHandler
{
    void SetNext(IHandler nextHandler);

    ICommand? Handle(string[] parts);
}
