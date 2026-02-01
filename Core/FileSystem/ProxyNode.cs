using System.IO;

namespace FileSystemManager.Core.FileSystem;

public abstract class ProxyNode : INode
{
    public string Name { get; }

    public string AbsolutePath { get; }

    private readonly INodeFactory _nodeFactory;

    private INode? _realNode;

    protected ProxyNode(string absolutePath, INodeFactory nodeFactory)
    {
        AbsolutePath = absolutePath;
        Name = Path.GetFileName(absolutePath);
        _nodeFactory = nodeFactory;
    }

    protected INode Load()
    {
        if (_realNode is null)
        {
            _realNode = _nodeFactory.CreateRealNode(AbsolutePath);
        }

        return _realNode;
    }
}
