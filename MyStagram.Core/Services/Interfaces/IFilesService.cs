using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Models.Helpers.File;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IFilesService : IReadOnlyFilesService
    {
        Task<FileModel> UploadFile(IFormFile file, string filePath, string fileExtension);
        void Delete(string path);
        void DeleteByFullPath(string path);
        void DeleteDirecetory(string folderpath);
    }
}