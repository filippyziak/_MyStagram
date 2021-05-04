using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Social;

namespace MyStagram.Core.Logic.Requests.Command.Social
{
    public class WatchFollowerRequest : IRequest<WatchFollowerResponse>
    {
        [Required]
        public string SenderId { get; set; }

    }
}