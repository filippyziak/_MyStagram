using System;
using System.Collections.Generic;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Models.Dtos.Auth
{
    public class UserAuthDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public string PhotoUrl { get; set; }
        public bool EmailConfirmed { get; set; }

        public ICollection<UserRoleDto> UserRoles { get; set; }


    }
}