using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Social;

namespace MyStagram.Core.Logic.Requests.Command.Social
{
    public class AcceptFollowerRequest : IRequest<AcceptFollowerResponse>
    {
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string RecipientId { get; set; }
        [Required]
        public bool Accepted { get; set; }
    }
}