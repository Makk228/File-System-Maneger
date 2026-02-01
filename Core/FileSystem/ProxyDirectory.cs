using System.Collections.Generic;

namespace FileSystemManager.Core.FileSystem;

public class ProxyDirectory : ProxyNode, IDirectory
{
    public ProxyDirectory(string absolutePath, INodeFactory nodeFactory)
        : base(absolutePath, nodeFactory)
    { }

    public IEnumerable<INode> GetChildren()
    {
        var real = (IDirectory)Load();
        return real.GetChildren();
    }
}
