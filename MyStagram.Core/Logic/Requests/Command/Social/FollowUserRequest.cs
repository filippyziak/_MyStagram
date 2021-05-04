using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Social;

namespace MyStagram.Core.Logic.Requests.Command.Social
{
    public class FollowUserRequest : IRequest<FollowUserResponse>
    {
        [Required]
        public string RecipientId { get; set; }  
    }
}