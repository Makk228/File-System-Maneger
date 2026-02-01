using FileSystemManager.Core.ResultTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemManager.Core.Commands;

public abstract class FileCommandBase
{
    protected IResulType EnsureConnected(SessionContext context)
    {
        if (!context.IsConnected || context.AbsolutPath is null || context.LocalPath is null)
            return new FailResult("Not connected. Please connect first.");

        if (context.NodeFactory is null)
            return new FailResult("Invalid session state: NodeFactory is null.");

        return new SuccessResult(string.Empty);
    }

    protected string ResolvePhysicalPath(SessionContext context, string path)
    {
        string baseUnix = context.LocalPath ?? "/";
        string fullUnix = NormalizeUnixPath(baseUnix, path);
        if (context.AbsolutPath is null)
            throw new InvalidOperationException("AbsolutPath is null in connected context.");
        return CombineRootAndUnix(context.AbsolutPath, fullUnix);
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
            effective = basePath.EndsWith('/')
                ? basePath + path
                : basePath + "/" + path;
        }

        string[] segments = effective.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var stack = new Stack<string>();

        foreach (string seg in segments)
        {
            if (seg is "." or "")
                continue;

            if (seg == "..")
            {
                if (stack.Count > 0) stack.Pop();
                continue;
            }

            stack.Push(seg);
        }

        return stack.Count == 0 ? "/" : "/" + string.Join("/", stack.Reverse());
    }

    private static string CombineRootAndUnix(string root, string unixPath)
    {
        if (unixPath == "/") return root;

        string relative = unixPath.TrimStart('/');
        string resultType = root;

        foreach (string segment in relative.Split('/', StringSplitOptions.RemoveEmptyEntries))
            resultType = Path.Combine(resultType, segment);

        return resultType;
    }
}
