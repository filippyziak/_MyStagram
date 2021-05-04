using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MyStagram.Core.Enums;
using MyStagram.Core.Helpers;

namespace MyStagram.Core.Models.Domain.Auth
{
    public class Role : IdentityRole<string>
    {
        public override string Id { get; set; } = Utils.Id();

        public virtual ICollection<UserRole> UserRoles { get; protected set; } = new HashSet<UserRole>();

        public static Role Create(RoleName roleName) => new Role { Name = Utils.EnumToString<RoleName>(roleName) };
    }
}