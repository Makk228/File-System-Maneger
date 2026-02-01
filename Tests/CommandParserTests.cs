using FileSystemManager.Core.Commands;
using FileSystemManager.Presentation.Parser;
using Xunit;

namespace FileSystemManager.Tests;

public class CommandParserTests
{
    private readonly CommandParser _parser = new CommandParser();

    [Fact]
    public void Parse_ConnectWithPath_ShouldCreateConnectCommandWithSamePath()
    {
        const string input = "connect /root";

        ICommand command = _parser.Parse(input);

        ConnectCommand connect = Assert.IsType<ConnectCommand>(command);
        Assert.Equal("/root", connect.AbsolutPath);
    }

    [Fact]
    public void Parse_Disconnect_ShouldCreateDisconnectCommand()
    {
        const string input = "disconnect";

        ICommand command = _parser.Parse(input);

        Assert.IsType<DisconnectCommand>(command);
    }

    [Fact]
    public void Parse_ConnectWithoutPath_ShouldCreateInvalidCommand()
    {
        const string input = "connect";

        ICommand command = _parser.Parse(input);

        Assert.IsType<InvalidCommand>(command);
    }

    [Fact]
    public void Parse_TreeGotoAbsolute_ShouldCreateTreeGotoCommandWithSamePath()
    {
        const string input = "tree goto /src";

        ICommand command = _parser.Parse(input);

        TreeGotoCommand treeGoto = Assert.IsType<TreeGotoCommand>(command);
        Assert.Equal("/src", treeGoto.Path);
    }

    [Fact]
    public void Parse_TreeGotoRelative_ShouldCreateTreeGotoCommandWithSamePath()
    {
        const string input = "tree goto subdir";

        ICommand command = _parser.Parse(input);

        TreeGotoCommand treeGoto = Assert.IsType<TreeGotoCommand>(command);
        Assert.Equal("subdir", treeGoto.Path);
    }

    [Fact]
    public void Parse_TreeListWithoutDepth_ShouldCreateTreeListCommandWithDefaultDepth1()
    {
        const string input = "tree list";

        ICommand command = _parser.Parse(input);

        TreeListCommand treeList = Assert.IsType<TreeListCommand>(command);
        Assert.Equal(1, treeList.Depth);
    }

    [Fact]
    public void Parse_TreeListWithDepth_ShouldCreateTreeListCommandWithGivenDepth()
    {
        const string input = "tree list -d 3";

        ICommand command = _parser.Parse(input);

        TreeListCommand treeList = Assert.IsType<TreeListCommand>(command);
        Assert.Equal(3, treeList.Depth);
    }

    [Fact]
    public void Parse_TreeListWithInvalidDepth_ShouldFallbackToDepth1()
    {
        const string input = "tree list -d abc";

        ICommand command = _parser.Parse(input);

        TreeListCommand treeList = Assert.IsType<TreeListCommand>(command);
        Assert.Equal(1, treeList.Depth);
    }

    [Fact]
    public void Parse_FileShow_ShouldCreateFileShowCommandWithSamePath()
    {
        const string input = "file show /file.txt";

        ICommand command = _parser.Parse(input);

        FileShowCommand show = Assert.IsType<FileShowCommand>(command);
        Assert.Equal("/file.txt", show.Path);
    }

    [Fact]
    public void Parse_FileCopy_ShouldCreateFileCopyCommandWithSourceAndTarget()
    {
        const string input = "file copy /a.txt /b.txt";

        ICommand command = _parser.Parse(input);

        FileCopyCommand copy = Assert.IsType<FileCopyCommand>(command);
        Assert.Equal("/a.txt", copy.SourcePath);
        Assert.Equal("/b.txt", copy.TargetPath);
    }

    [Fact]
    public void Parse_FileMove_ShouldCreateFileMoveCommandWithSourceAndTarget()
    {
        const string input = "file move /a.txt /b.txt";

        ICommand command = _parser.Parse(input);

        FileMoveCommand move = Assert.IsType<FileMoveCommand>(command);
        Assert.Equal("/a.txt", move.SourcePath);
        Assert.Equal("/b.txt", move.TargetPath);
    }

    [Fact]
    public void Parse_FileDelete_ShouldCreateFileDeleteCommandWithSamePath()
    {
        const string input = "file delete /a.txt";

        ICommand command = _parser.Parse(input);

        FileDeleteCommand del = Assert.IsType<FileDeleteCommand>(command);
        Assert.Equal("/a.txt", del.Path);
    }

    [Fact]
    public void Parse_FileRename_ShouldCreateFileRenameCommandWithPathAndName()
    {
        const string input = "file rename /a.txt b.txt";

        ICommand command = _parser.Parse(input);

        FileRenameCommand rename = Assert.IsType<FileRenameCommand>(command);
        Assert.Equal("/a.txt", rename.Path);
        Assert.Equal("b.txt", rename.Name);
    }

    [Fact]
    public void Parse_FileCopyWithMissingArgs_ShouldCreateInvalidCommand()
    {
        const string input = "file copy /a.txt";

        ICommand command = _parser.Parse(input);

        Assert.IsType<InvalidCommand>(command);
    }

    [Fact]
    public void Parse_UnknownCommand_ShouldCreateInvalidCommand()
    {
        const string input = "abracadabra";

        ICommand command = _parser.Parse(input);

        Assert.IsType<InvalidCommand>(command);
    }

    [Fact]
    public void Parse_EmptyOrWhitespace_ShouldCreateInvalidCommand()
    {
        const string input = "   ";

        ICommand command = _parser.Parse(input);

        Assert.IsType<InvalidCommand>(command);
    }
}
