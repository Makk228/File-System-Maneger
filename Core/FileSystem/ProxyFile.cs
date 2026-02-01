namespace FileSystemManager.Core.FileSystem;

public class ProxyFile : ProxyNode, IFile
{
    public ProxyFile(string absolutePath, INodeFactory nodeFactory) : base(absolutePath, nodeFactory)
    {
    }

    public string ReadContent()
    {
        var file = (IFile)Load();
        return file.ReadContent();
    }
}
