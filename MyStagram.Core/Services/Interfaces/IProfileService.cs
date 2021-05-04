using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IProfileService : IReadOnlyProfileService
    {
        Task<UpdateProfileResult> UpdateProfile(string newUserName, string newName, string newSurname, string newDescription, string newEmail, bool privacy);
        Task<ChangePasswordResult> ChangePassword(string oldPassword, string newPassword);
        Task<string> SetAvatar(IFormFile photo);
        Task<bool> DeleteAvatar();
    }
}