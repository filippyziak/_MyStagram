using System.Threading.Tasks;

namespace MyStagram.Core.Logging
{
    public interface ILogManager : IReadOnlyLogManager
    {
        Task ClearLogs();
        Task<bool> StoreLogs();
    }
}