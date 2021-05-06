using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyStagram.Core.Extensions;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Helpers;
using System.IO;
using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Logic.Requests.Query.Profile;
using Microsoft.EntityFrameworkCore;
using MyStagram.Core.Data;
using MyStagram.Core.Data.Models;

namespace MyStagram.Core.Services.Interfaces
{

    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;
        private readonly IDatabase database;
        private readonly IFilesService filesService;
        public ProfileService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IDatabase database,
                            IFilesService filesService)
        {
            this.database = database;
            this.userManager = userManager;
            this.currentUserId = httpContextAccessor.HttpContext?.GetCurrentUserId();
            this.filesService = filesService;

        }


        public async Task<User> GetCurrentUser()
        => await userManager.FindByIdAsync(currentUserId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

        public async Task<User> GetUser(string userId)
        => await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

        public async Task<IPagedList<User>> GetProfiles(GetProfilesRequest request)
        => await database.UserRepository.GetProfiles(request);
        public async Task<UpdateProfileResult> UpdateProfile(string newUserName, string newName, string newSurname, string newDescription, string newEmail, bool privacy)
        {
            var user = await GetCurrentUser();
            user.UpdateProfile(newUserName, newSurname, newName, newDescription, newEmail);
            user.ChangePrivacy(privacy);
            await database.Complete();

            return new UpdateProfileResult(newUserName, newSurname, newName, newDescription, newEmail, user.IsPrivate);
        }

        public async Task<ChangePasswordResult> ChangePassword(string oldPassword, string newPassword)
        {
            var user = await GetCurrentUser();
            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result.Succeeded ? new ChangePasswordResult(true) : new ChangePasswordResult(false, result.ToString());
        }

        public async Task<string> SetAvatar(IFormFile photo)
        {
            var user = await GetCurrentUser();

            string currentPath = $"files/avatars/{user.Id}";
            filesService.DeleteDirecetory(currentPath);
            await database.FileRepository.DeleteFileByPath(currentPath);

            var uploadAvatar = await filesService.UploadFile(photo, $"avatars/{user.Id}", Path.GetExtension(photo.FileName));

            user.SetAvatar(uploadAvatar.FileUrl);
            database.FileRepository.AddFile(uploadAvatar.FileUrl, uploadAvatar.FilePath);

            await database.Complete();

            return user.PhotoUrl;
        }

        public async Task<bool> DeleteAvatar()
        {
            var user = await GetCurrentUser();

            if (string.IsNullOrEmpty(user.PhotoUrl))
                return false;

            string path = $"files/avatars/{user.Id}";
            filesService.DeleteDirecetory(path);
            await database.FileRepository.DeleteFileByPath(path);

            user.SetAvatar(null);

            return await database.Complete();
        }
    }
}