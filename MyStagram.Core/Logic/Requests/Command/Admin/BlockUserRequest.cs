using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Admin;

namespace MyStagram.Core.Logic.Requests.Command.Admin
{
    public class BlockUserRequest : IRequest<BlockUserResponse>
    {
        [Required]
        public string UserId { get; set; }

    }
}