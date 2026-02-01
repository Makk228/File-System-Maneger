namespace FileSystemManager.Core.FileSystem;

public interface IFile : INode
{
    string ReadContent();
}
