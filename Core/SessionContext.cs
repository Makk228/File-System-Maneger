using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.SystemAbstraction;
using FileSystemManager.Core.ResultTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemManager.Core;

public class SessionContext : ISessionContext
{
    public bool IsConnected { get; private set; }

    public string? LocalPath { get; private set; }

    public IDirectory? CurrentDirectory { get; private set; }

    public INodeFactory? NodeFactory { get; private set; }

    public IFileSystem FileSystem { get; }

    public string? AbsolutPath { get; private set; }

    public SessionContext(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
        IsConnected = false;
    }

    public IResulType Connect(string absolutPath)
    {
        if (IsConnected)
            return new FailResult("Already connected. Please disconnect first.");

        if (!FileSystem.DirectoryExists(absolutPath))
            return new FailResult($"Path to directory is incorrect: {absolutPath}");

        IsConnected = true;
        AbsolutPath = absolutPath;
        LocalPath = "/";
        NodeFactory = new NodeFactory(FileSystem);

        CurrentDirectory = (IDirectory)NodeFactory.CreateProxyNode(absolutPath);

        return new SuccessResult($"Connected to directory: {absolutPath}");
    }

    public IResulType Disconnect()
    {
        if (!IsConnected)
            return new FailResult("Not connected.");

        IsConnected = false;
        AbsolutPath = null;
        LocalPath = null;
        NodeFactory = null;
        CurrentDirectory = null;

        return new SuccessResult("Disconnected successfully.");
    }

    public IResulType ChangeDirectory(string path)
    {
        if (!IsConnected)
            return new FailResult("Not connected. Please connect first.");

        if (AbsolutPath is null || LocalPath is null || NodeFactory is null)
            return new FailResult("Invalid session state.");

        string newLocalUnix = NormalizeUnixPath(LocalPath, path);

        string fullPhysical = CombineRootAndUnix(AbsolutPath, newLocalUnix);

        if (!FileSystem.DirectoryExists(fullPhysical))
            return new FailResult($"Directory does not exist: {newLocalUnix}");

        LocalPath = newLocalUnix;
        CurrentDirectory = (IDirectory)NodeFactory.CreateProxyNode(fullPhysical);

        return new SuccessResult($"Moved to: {newLocalUnix}");
    }

    private static string NormalizeUnixPath(string basePath, string path)
    {
        string effective;

        if (string.IsNullOrEmpty(path))
        {
            effective = basePath;
        }
        else if (path.StartsWith('/'))
        {
            effective = path;
        }
        else
        {
            if (!basePath.EndsWith('/'))
                effective = basePath + "/" + path;
            else
                effective = basePath + path;
        }

        string[] parts = effective.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var stack = new Stack<string>();

        foreach (string seg in parts)
        {
            if (seg == "." || seg.Length == 0)
                continue;

            if (seg == "..")
            {
                if (stack.Count > 0)
                    stack.Pop();
                continue;
            }

            stack.Push(seg);
        }

        if (stack.Count == 0)
            return "/";

        return "/" + string.Join("/", stack.Reverse());
    }

    private static string CombineRootAndUnix(string root, string unixPath)
    {
        if (unixPath == "/")
            return root;

        string relative = unixPath.TrimStart('/');
        string resultType = root;

        foreach (string segment in relative.Split('/', StringSplitOptions.RemoveEmptyEntries))
            resultType = Path.Combine(resultType, segment);

        return resultType;
    }
}
