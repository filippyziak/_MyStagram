using System.Threading.Tasks;

namespace MyStagram.Core.Data.Repositories
{
    public interface IFileRepository : IRepository<MyStagram.Core.Models.Domain.File.File>
    {
        void AddFile(string url, string path);
        Task DeleteFileByPath(string path);
    }
}