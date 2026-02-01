using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.SystemAbstraction;
using FileSystemManager.Core.ResultTypes;

namespace FileSystemManager.Core;

internal interface ISessionContext
{
    string? LocalPath { get; } // подмножество абсолютного пути

    IDirectory? CurrentDirectory { get; }

    INodeFactory? NodeFactory { get; }

    IFileSystem FileSystem { get; }

    string? AbsolutPath { get; }

    IResulType Connect(string absolutPath);

    IResulType Disconnect();

    IResulType ChangeDirectory(string path);
}
