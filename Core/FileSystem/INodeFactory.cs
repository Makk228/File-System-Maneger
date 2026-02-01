namespace FileSystemManager.Core.FileSystem;

public interface INodeFactory
{
    INode CreateRealNode(string absolutePath);

    INode CreateProxyNode(string absolutePath);
}
