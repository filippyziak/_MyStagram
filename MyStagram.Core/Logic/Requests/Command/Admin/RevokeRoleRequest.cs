using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Admin;

namespace MyStagram.Core.Logic.Requests.Command.Admin
{
    public class RevokeRoleRequest : IRequest<RevokeRoleResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleId { get; set; }

    }
}