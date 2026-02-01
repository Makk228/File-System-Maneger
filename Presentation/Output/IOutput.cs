namespace FileSystemManager.Presentation.Output;

public interface IOutput
{
    void WriteInfo(string message);

    void WriteError(string message);
}
