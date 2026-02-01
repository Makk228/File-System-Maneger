using FileSystemManager.Core.FileSystem.SystemAbstraction;
using System.IO;

namespace FileSystemManager.Core.FileSystem;

public class NodeFactory : INodeFactory
{
    private readonly IFileSystem _fileSystem;

    public NodeFactory(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public INode CreateProxyNode(string absolutePath)
    {
        if (_fileSystem.FileExists(absolutePath))
        {
            return new ProxyFile(absolutePath, this);
        }

        if (_fileSystem.DirectoryExists(absolutePath))
        {
            return new ProxyDirectory(absolutePath, this);
        }

        throw new FileNotFoundException();
    }

    public INode CreateRealNode(string absolutePath)
    {
        bool fileExists = _fileSystem.FileExists(absolutePath);
        bool directoryExists = _fileSystem.DirectoryExists(absolutePath);

        if (fileExists)
        {
            return new RealFile(absolutePath, _fileSystem);
        }

        if (directoryExists)
        {
            return new RealDirectory(absolutePath, this, _fileSystem);
        }

        throw new FileNotFoundException();
    }
}