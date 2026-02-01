using System.Collections.Generic;

namespace FileSystemManager.Core.FileSystem.SystemAbstraction;

// Во все методы передаётся абсолютный путь, понятный конкретной реализации (для LocalFileSystem — путь ОС).
public interface IFileSystem
{
    string ToSystemPath(string unixPath);

    bool Exists(string path);

    bool FileExists(string path);

    bool DirectoryExists(string path);

    IEnumerable<string> EnumerateEntries(string path);

    string ReadFile(string path);

    void WriteFile(string path, string content);

    void Copy(string from, string target);

    void Move(string from, string target);

    void Delete(string path);

    void CreateDirectory(string path);

    void Rename(string path, string newName);
}
