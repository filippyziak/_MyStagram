using System.Threading.Tasks;
using MyStagram.Core.Enums;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IRolesService : IReadOnlyRolesService
    {
        bool AdmitRole(string roleId, User user);
        Task<bool> CreateRole(RoleName roleName);
        bool RevokeRole(string roleId, User user);
    }
}