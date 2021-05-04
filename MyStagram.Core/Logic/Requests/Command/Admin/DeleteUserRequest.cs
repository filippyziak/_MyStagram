using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Admin;

namespace MyStagram.Core.Logic.Requests.Command.Admin
{
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        [Required]
        public string UserId { get; set; }

    }
}