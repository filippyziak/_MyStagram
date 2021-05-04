namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyFilesService
    {
        string ProjectPath { get; }
        string WebRootPath { get; }
        bool FileExists(string filePath);
    }
}