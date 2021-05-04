using System.Threading.Tasks;
using MyStagram.Core.Logic.Requests.Query.Admin;
using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Models.Mongo;

namespace MyStagram.Core.Logging
{
    public interface IReadOnlyLogManager
    {
        Task<PagedList<LogDocument>> GetLogs(GetLogsRequest paginationRequest);
    }
}