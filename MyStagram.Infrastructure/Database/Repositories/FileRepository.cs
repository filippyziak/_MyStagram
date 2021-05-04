using System.Threading.Tasks;
using MyStagram.Core.Data.Repositories;


namespace MyStagram.Infrastructure.Database.Repositories
{
    public class FileRepository : Repository<Core.Models.Domain.File.File>, IFileRepository
    {
        public FileRepository(DataContext context) : base(context) { }

        public void AddFile(string url, string path)
        {
            var fileToAdd = Core.Models.Domain.File.File.Create(path, url);
            Add(fileToAdd);
        }

        public async Task DeleteFileByPath(string path)
        {
            var fileToDelete = await Find(f => f.FilePath.ToLower().Contains(path.ToLower()));

            if (fileToDelete != null)
            {
                Delete(fileToDelete);
            }
        }
    }
}