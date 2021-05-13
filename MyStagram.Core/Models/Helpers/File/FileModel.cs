namespace MyStagram.Core.Models.Helpers.File
{
    public class FileModel
    {
        public string FilePath { get; }
        public string FileUrl { get; }
        public string FullPath { get; }

        public FileModel(string filePath, string fileUrl, string fullPath = null)
        {
            FilePath = filePath;
            FileUrl = fileUrl;
            FullPath = fullPath;
        }
    }
}