using System;
using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Data;
using MyStagram.Core.Enums;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Services
{

    public class RolesService : IRolesService
    {
        private readonly IDatabase database;

        public RolesService(IDatabase database)
        {
            this.database = database;
        }

        public bool AdmitRole(string roleId, User user)
        {
            if (user.UserRoles.Any(ur => ur.RoleId == roleId))
                return false;

            user.UserRoles.Add(UserRole.Create(user.Id, roleId));

            return true;
        }

        public bool RevokeRole(string roleId, User user)
        {
            var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);

            if (userRole == null)
                return false;

            if (userRole.Role.Name == Utils.EnumToString<RoleName>(RoleName.User))
                return false;

            user.UserRoles.Remove(userRole);

            return true;
        }

        public async Task<bool> CreateRole(RoleName roleName)
        {
            if (await RoleExists(roleName))
                return false;

            database.RoleRepository.Add(Role.Create(roleName));

            return await database.Complete();
        }
        public async Task<bool> RoleExists(RoleName roleName)
        => await GetRoleId(roleName) != null;

        public async Task<string> GetRoleId(RoleName roleName)
        => (await database.RoleRepository.Find(r => r.Name.ToLower() == Utils.EnumToString<RoleName>(roleName).ToLower()))?.Id;

        public bool IsPermitted(User user, params RoleName[] roleNames)
            => user.UserRoles.Any(ur => roleNames.Contains(Enum.Parse<RoleName>(ur.Role.Name)));

        public async Task<bool> IsPermitted(string userId, params RoleName[] roleNames)
            => (await database.UserRepository.Get(userId)).UserRoles.Any(ur => roleNames.Contains(Enum.Parse<RoleName>(ur.Role.Name)));

        public static bool IsAdmin(User user)
        => user.UserRoles.Any(ur => ur.Role.Name == Constants.AdminRole
        || ur.Role.Name == Constants.HeadAdminRole);

        public static bool IsHeadAdmin(User user)
        => user.UserRoles.Any(ur => ur.Role.Name == Constants.HeadAdminRole);

    }
}