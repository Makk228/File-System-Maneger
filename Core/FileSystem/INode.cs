namespace FileSystemManager.Core.FileSystem;

public interface INode
{
    string Name { get; }

    string AbsolutePath { get; }
}
