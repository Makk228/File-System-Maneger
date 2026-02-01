using FileSystemManager.Core.Commands;
using System;

namespace FileSystemManager.Presentation.Parser;

public sealed class CommandParser
{
    private readonly ConnectCommandHandler _rootHandler;

    public CommandParser()
    {
        // создаём все хендлеры
        var connectHandler = new ConnectCommandHandler();
        var disconnectHandler = new DisconnectCommandHandler();
        var treeGotoHandler = new TreeGotoCommandHandler();
        var treeListHandler = new TreeListCommandHandler();
        var fileShowHandler = new FileShowCommandHandler();
        var fileCopyHandler = new FileCopyCommandHandler();
        var fileMoveHandler = new FileMoveCommandHandler();
        var fileDeleteHandler = new FileDeleteCommandHandler();
        var fileRenameHandler = new FileRenameCommandHandler();
        var invalidCommandHandler = new InvalidCommandHandler();

        // строим цепочку
        connectHandler.SetNext(disconnectHandler);
        disconnectHandler.SetNext(treeGotoHandler);
        treeGotoHandler.SetNext(treeListHandler);
        treeListHandler.SetNext(fileShowHandler);
        fileShowHandler.SetNext(fileCopyHandler);
        fileCopyHandler.SetNext(fileMoveHandler);
        fileMoveHandler.SetNext(fileDeleteHandler);
        fileDeleteHandler.SetNext(fileRenameHandler);
        fileRenameHandler.SetNext(invalidCommandHandler);

        _rootHandler = connectHandler;
    }

    public ICommand Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new InvalidCommand("Command is empty.");

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        ICommand? command = _rootHandler.Handle(parts);

        return command ?? new InvalidCommand("Unknown command.");
    }
}
