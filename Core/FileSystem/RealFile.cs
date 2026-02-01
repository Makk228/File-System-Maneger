using FileSystemManager.Core.FileSystem.SystemAbstraction;
using System.IO;

namespace FileSystemManager.Core.FileSystem;

public class RealFile : IFile
{
    public string Name { get; }

    public string AbsolutePath { get; }

    private readonly IFileSystem _fileSystem;

    public RealFile(string absolutePath, IFileSystem fileSystem)
    {
        AbsolutePath = absolutePath;
        Name = Path.GetFileName(absolutePath);
        _fileSystem = fileSystem;
    }

    public string ReadContent()
    {
        return _fileSystem.ReadFile(AbsolutePath);
    }
}
