namespace MyStagram.Core.Models.Helpers.File
{
    public class FileModel
    {
        public string FilePath { get; }
        public string FileUrl { get; }

        public FileModel(string filePath, string fileUrl)
        {
            FilePath = filePath;
            FileUrl = fileUrl;
        }
    }
}