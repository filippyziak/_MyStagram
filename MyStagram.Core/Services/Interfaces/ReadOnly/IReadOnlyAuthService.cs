using System.Threading.Tasks;

namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyAuthService
    {
        Task<bool> EmailExists(string email);
        Task<bool> UserNameExists(string userName);
    }
}