using FileSystemManager.Core.FileSystem.SystemAbstraction;
using System.Collections.Generic;
using System.IO;

namespace FileSystemManager.Core.FileSystem;

public class RealDirectory : IDirectory
{
    private readonly IFileSystem _fileSystem;
    private readonly INodeFactory _factory;

    public RealDirectory(string absolutePath, INodeFactory nodeFactory, IFileSystem fileSystem)
    {
        AbsolutePath = absolutePath;
        Name = Path.GetFileName(absolutePath);
        _fileSystem = fileSystem;
        _factory = nodeFactory;
    }

    public string Name { get; }

    public string AbsolutePath { get; }

    public IEnumerable<INode> GetChildren()
    {
        foreach (string fileName in _fileSystem.EnumerateEntries(AbsolutePath))
        {
            yield return _factory.CreateProxyNode(fileName);
        }
    }
}
