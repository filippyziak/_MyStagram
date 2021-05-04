using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Helpers.Result
{
    public class SignUpResult
    {

        public User User { get; }
        public SignUpResult(User user)
        {
            User = user;
        }
    }
}