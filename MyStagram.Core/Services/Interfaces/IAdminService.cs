using System.Threading.Tasks;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IAdminService
    {
        Task<bool> AdmitRole(string userId, string roleId);
        Task<bool> BlockUser(string userId);
        Task<bool> DeleteUser(string userId);
        Task<bool> RevokeRole(string userId, string roleId);

    }
}