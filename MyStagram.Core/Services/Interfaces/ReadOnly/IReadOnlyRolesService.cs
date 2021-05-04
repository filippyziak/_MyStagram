using System.Threading.Tasks;
using MyStagram.Core.Enums;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyRolesService
    {
        Task<string> GetRoleId(RoleName roleName);
        Task<bool> RoleExists(RoleName roleName);

        bool IsPermitted(User user, params RoleName[] roleNames);
        Task<bool> IsPermitted(string userId, params RoleName[] roleNames);
    }
}