using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Messenger;

namespace MyStagram.Core.Logic.Requests.Command.Messenger
{
    public class ReadMessageRequest : IRequest<ReadMessageResponse>
    {
        [Required]
        public string MessageId { get; set; }
        
    }
}