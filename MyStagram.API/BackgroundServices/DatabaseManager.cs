using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using MyStagram.API.BackgroundServices.Interfaces;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace MyStagram.API.BackgroundServices
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IRolesService rolesService;
        private readonly UserManager<User> userManager;

        public DatabaseManager(IRolesService rolesService, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.rolesService = rolesService;
        }
        public void Seed()
        {
            InsertRoles();
            InsertUsers();
        }

        #region private

        private void InsertRoles()
        {
            Constants.RolesToSeed.ToList().ForEach(roleName =>
            {
                rolesService.CreateRole(roleName).Wait();
            });
        }

        private void InsertUsers()
        {
            var users = JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(@"D:\.projects\MyStagramApp\MyStagram.API\wwwroot\files\data\UserSeedData.json"));

            foreach(var user in users)
            {
                userManager.CreateAsync(user, "password").Wait();
            }
        }

        #endregion
    }
}