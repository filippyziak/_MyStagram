using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Admin;

namespace MyStagram.Core.Logic.Requests.Command.Admin
{
    public class AdmitRoleRequest : IRequest<AdmitRoleResponse>
    {
        [Required]
        public string RoleId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}