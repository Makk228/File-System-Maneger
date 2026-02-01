using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemManager.Core.FileSystem.SystemAbstraction;

public class LocalFileSystem : IFileSystem
{
    public string ToSystemPath(string unixPath)
    {
        return unixPath.Replace('/', Path.DirectorySeparatorChar);
    }

    public void Copy(string from, string target)
    {
        from = ToSystemPath(from);
        target = ToSystemPath(target);

        if (!Exists(from))
        {
            throw new FileNotFoundException($"Source file not found: {from}");
        }

        if (FileExists(from))
        {
            if (Directory.Exists(target))
            {
                string fileName = Path.GetFileName(from);
                string destinationPath = Path.Combine(target, fileName);
                File.Copy(from, destinationPath, overwrite: true);
            }
            else
            {
                File.Copy(from, target, overwrite: true);
            }

            return;
        }

        if (DirectoryExists(from))
        {
            string fromFull = Path.GetFullPath(from).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string targetFull = Path.GetFullPath(target).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            if (targetFull.Equals(fromFull, StringComparison.OrdinalIgnoreCase) ||
                targetFull.StartsWith(fromFull + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Cannot copy a directory into itself or its subdirectory.");
            }

            if (!Directory.Exists(targetFull))
            {
                Directory.CreateDirectory(targetFull);
            }

            var directoryInfo = new DirectoryInfo(from);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                string tergetFileParh = Path.Combine(targetFull, file.Name);
                file.CopyTo(tergetFileParh, overwrite: true);
            }

            foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
            {
                string tergetSubDirPath = Path.Combine(targetFull, subDirectory.Name);
                Copy(subDirectory.FullName, tergetSubDirPath);
            }

            return;
        }
    }

    public void CreateDirectory(string path)
    {
        path = ToSystemPath(path);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public void Delete(string path)
    {
        path = ToSystemPath(path);

        if (DirectoryExists(path))
        {
            Directory.Delete(path, true);
        }
        else if (FileExists(path))
        {
            File.Delete(path);
        }
    }

    public IEnumerable<string> EnumerateEntries(string path)
    {
        path = ToSystemPath(path);

        return Directory.EnumerateFileSystemEntries(path);
    }

    public bool Exists(string path)
    {
        path = ToSystemPath(path);
        return FileExists(path) || DirectoryExists(path);
    }

    public bool DirectoryExists(string path)
    {
        path = ToSystemPath(path);
        return Directory.Exists(path);
    }

    public bool FileExists(string path)
    {
        path = ToSystemPath(path);
        return File.Exists(path);
    }

    public void Move(string from, string target)
    {
        from = ToSystemPath(from);
        target = ToSystemPath(target);

        if (FileExists(from))
            File.Move(from, target, overwrite: true);
        else
            Directory.Move(from, target);
    }

    public string ReadFile(string path)
    {
        path = ToSystemPath(path);

        return File.ReadAllText(path);
    }

    public void WriteFile(string path, string content)
    {
        path = ToSystemPath(path);

        File.WriteAllText(path, content);
    }

    public void Rename(string path, string newName)
    {
        path = ToSystemPath(path);

        if (!File.Exists(path) && !Directory.Exists(path))
            throw new FileNotFoundException($"Path not found: {path}");

        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("New name must not be empty.", nameof(newName));

        if (newName.Contains(Path.DirectorySeparatorChar, StringComparison.Ordinal) ||
            newName.Contains(Path.AltDirectorySeparatorChar, StringComparison.Ordinal))
        {
            throw new ArgumentException(
                "New name must not contain directory separators.",
                nameof(newName));
        }

        string? directory = Path.GetDirectoryName(path) ?? throw new InvalidOperationException(
                $"Cannot determine parent directory for '{path}'.");
        string targetPath = Path.Combine(directory, newName);

        if (File.Exists(path))
            File.Move(path, targetPath, overwrite: true);
        else
            Directory.Move(path, targetPath);
    }
}
