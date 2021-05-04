using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Social;

namespace MyStagram.Core.Logic.Requests.Command.Social
{
    public class UnFollowUserRequest : IRequest<UnFollowUserResponse>
    {
        [Required]
        public string RecipientId { get; set; }
    }
}