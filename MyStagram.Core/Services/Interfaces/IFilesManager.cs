using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Models.Helpers.File;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IFilesManager : IReadOnlyFilesService
    {
        Task<FileModel> Upload(IFormFile file, string filePath = null);
        void Delete(string path);
        void DeleteDirectory(string path, bool isRecursive = true);
    }
}