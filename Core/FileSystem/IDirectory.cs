using System.Collections.Generic;

namespace FileSystemManager.Core.FileSystem;

public interface IDirectory : INode
{
    IEnumerable<INode> GetChildren();
}
