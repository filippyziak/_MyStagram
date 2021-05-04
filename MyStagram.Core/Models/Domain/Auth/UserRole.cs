using Microsoft.AspNetCore.Identity;

namespace MyStagram.Core.Models.Domain.Auth
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; protected set; }
        public virtual Role Role { get; protected set; }
        public static UserRole Create(string userId, string roleId) => new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };
    }
}