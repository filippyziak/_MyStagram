using System.Threading.Tasks;
using MyStagram.Core.Data;
using MyStagram.Core.Enums;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services
{

    public class AdminService : IAdminService
    {
        private readonly IDatabase database;
        private readonly IRolesService rolesService;
        private readonly IReadOnlyProfileService profileService;

        public AdminService(IDatabase database, IRolesService rolesService, IReadOnlyProfileService profileService)
        {
            this.database = database;
            this.rolesService = rolesService;
            this.profileService = profileService;
        }

        public async Task<bool> AdmitRole(string userId, string roleId)
        {
            var currentAdmin = await GetAdmin(userId);

            var user = await GetUserToManage(userId);

            return rolesService.AdmitRole(roleId, user) ? await database.Complete() : false;
        }

        public async Task<bool> RevokeRole(string userId, string roleId)
        {
            var currentAdmin = await GetAdmin(userId);

            var user = await GetUserToManage(userId);

            return rolesService.RevokeRole(roleId, user) ? await database.Complete() : false;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var currentAdmin = await GetAdmin(userId);

            var user = await GetUserToManage(userId);

            database.UserRepository.Delete(user);

            return await database.Complete();
        }

        public async Task<bool> BlockUser(string userId)
        {
            var admin = await GetAdmin(userId);

            var user = await GetUserToManage(userId);

            user.BlockUser();

            await database.Complete();

            return user.IsBlocked;
        }

        private async Task<User> GetAdmin(string userId)
        {
            var currentAdmin = await profileService.GetCurrentUser();

            if (!RolesService.IsAdmin(currentAdmin))
                throw new NoPermissionsException("You are not allowed to perform this action");

            if (currentAdmin.Id == userId)
                throw new NoPermissionsException("You do not have permissions to manage your account");

            return currentAdmin;
        }

        private async Task<User> GetUserToManage(string userId)
        {
            var user = await profileService.GetUser(userId);
            var currentAdmin = await GetAdmin(userId);

            if (RolesService.IsAdmin(user) && !rolesService.IsPermitted(currentAdmin, RoleName.HeadAdmin))
                throw new NoPermissionsException("You do not have permissions to manage admin account");

            return user;
        }
    }
}