using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Helpers.Result
{
    public class LoginResult
    {

        public string Token { get; }
        public User User { get; }
        public LoginResult(string token, User user)
        {
            Token = token;
            User = user;
        }
    }
}