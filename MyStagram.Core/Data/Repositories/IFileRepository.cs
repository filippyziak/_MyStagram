using System.Threading.Tasks;

namespace MyStagram.Core.Data.Repositories
{
    public interface IFileRepository : IRepository<Models.Domain.File.File>
    {
        void AddFile(string url, string path);
        Task DeleteFileByPath(string path);
    }
}