using FileSystemManager.Core;
using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem.SystemAbstraction;
using FileSystemManager.Core.ResultTypes;
using FileSystemManager.Presentation.Output;
using FileSystemManager.Presentation.Parser;
using System;

namespace FileSystemManager.Presentation;

internal static class Program
{
    private static void Main()
    {
        var fileSystem = new LocalFileSystem();
        var session = new SessionContext(fileSystem);
        var parser = new CommandParser();
        var output = new ConsoleOutput();

        Console.WriteLine("Lab 4 File Manager. Type 'exit' to quit.");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (input is null)
                continue;

            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            ICommand command = parser.Parse(input);
            IResulType resultType = command.Execute(session);

            if (resultType is FailResult)
                output.WriteError(resultType.ResultType);
            else
                output.WriteInfo(resultType.ResultType);
        }
    }
}
