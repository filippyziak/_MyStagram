using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Messenger;

namespace MyStagram.Core.Logic.Requests.Command.Messenger
{
    public class MessageSendRequest : IRequest<MessageSendResponse>
    {
        [Required]
        public string RecipientId { get; set; }

        [Required]
        public string Content { get; set; } 
    }
}