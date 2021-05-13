using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Helpers.File;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Infrastructure.Upload
{
    public class FilesService : IFilesManager
    {
        public string ProjectPath => webHostEnvironment.ContentRootPath;
        public string WebRootPath => webHostEnvironment.WebRootPath;

        private readonly IWebHostEnvironment webHostEnvironment;
        public IConfiguration Configuration { get; }

        public FilesService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.Configuration = configuration;
        }
        public async Task<FileModel> Upload(IFormFile file, string filePath = null)
            => await UploadFile(file, filePath);


        public void Delete(string path)
        {
            if (!FileExists(path))
                return;

            path = string.IsNullOrEmpty(path) ? $"{WebRootPath}/" : $"{WebRootPath}/{path}";

            File.Delete(path);
        }

        public void DeleteDirectory(string path, bool isRecursive = true)
        {
            path = string.IsNullOrEmpty(path) ? $"{WebRootPath}/" : $"{WebRootPath}/{path}";

            if (Directory.Exists(path))
                Directory.Delete(path, recursive: isRecursive);
        }

        public bool FileExists(string filePath)
            => File.Exists($"{WebRootPath}/{filePath}");


        private async Task<FileModel> UploadFile(IFormFile file, string filePath)
        {
            if (file == null || file.Length <= 0)
                return null;

            var uploadFile = BuildFileModel(filePath, Path.GetExtension(file.FileName));

            using (var stream = System.IO.File.Create(uploadFile.FullPath))
            {
                await file.CopyToAsync(stream);
            }

            return uploadFile;
        }

        private FileModel BuildFileModel(string filePath, string fileExtension)
        {
            var (relativePath, fullPath) = ($"/files/{filePath}/", $"{WebRootPath}/files/{filePath}/");
            var fileUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}/files/{filePath}/";

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            string fileName = $"{Utils.NewGuid(length: 32)}{fileExtension}";

            relativePath += fileName;
            fullPath += fileName;
            fileUrl += fileName;

            return new FileModel(relativePath, fileUrl, fullPath);
        }
    }
}