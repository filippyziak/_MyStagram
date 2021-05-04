using System.Threading.Tasks;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IAuthService : IReadOnlyAuthService
    {
        Task<LoginResult> SignIn(string email, string password);
        Task<SignUpResult> SignUp(string email, string password, string userName);
        Task<string> GeneratedConfirmEmailUrl(string userId);
        Task<bool> ConfirmAccount(string userId, string token);
        Task<bool> ResetPassword(string userId, string newPassword, string token);
        Task<string> GenerateResetPasswordUrl(string email);
        Task<bool> ConfirmResetPassword(string userId, string token);
    }
}