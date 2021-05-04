using System;
using MyStagram.Core.Helpers;

namespace MyStagram.Core.Models.Domain.File
{
    public class File
    {
        public string Id { get; protected set; } = Utils.Id();
        public string FilePath { get; protected set; }
        public string FileUrl { get; protected set; }
        public DateTime Created { get; protected set; } = DateTime.Now;

        public static File Create(string filePath, string fileUrl) => new File { FilePath = filePath, FileUrl = fileUrl };
    }
}