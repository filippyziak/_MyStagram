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
    public class FilesService : IFilesService
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
        public async Task<FileModel> UploadFile(IFormFile file, string filePath, string fileExtension)
        {
            if (file?.Length <= 0)
                return null;

            var generatedPath = filePath != null ? $"{webHostEnvironment.WebRootPath}/files/{filePath}/" : $"{webHostEnvironment.WebRootPath}/files/";
            var fileName = $"{Utils.NewGuid(length: 32)}{fileExtension}";
            var generatedUrl = filePath != null ? $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}files/{filePath}/" : $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}files/";

            if (!Directory.Exists(generatedPath))
                Directory.CreateDirectory(generatedPath);

            generatedPath += fileName;
            generatedUrl += fileName;
            FileModel upload = new FileModel(generatedPath, generatedUrl);

            using (var stream = System.IO.File.Create(upload.FilePath))
            {
                await file.CopyToAsync(stream);
            }
            return upload;
        }

        public void Delete(string path)
        {
            path = string.IsNullOrEmpty(path) ? $"{webHostEnvironment.WebRootPath}/" : $"{webHostEnvironment.WebRootPath}/{path}";

            if (FileExists(path))
                File.Delete(path);

        }

        public void DeleteByFullPath(string path)
        {
            if (FileExists(path))
                File.Delete(path);
        }

        public void DeleteDirecetory(string folderpath)
        {
            folderpath = string.IsNullOrEmpty(folderpath) ? $"{webHostEnvironment.WebRootPath}/" : $"{webHostEnvironment.WebRootPath}/{folderpath}";

            if (Directory.Exists(folderpath))
                Directory.Delete(folderpath, true);
        }

        public bool FileExists(string filePath)
            => File.Exists(filePath);

    }
}