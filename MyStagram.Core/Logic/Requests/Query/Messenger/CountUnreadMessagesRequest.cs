using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Messenger;

namespace MyStagram.Core.Logic.Requests.Query.Messenger
{
    public class CountUnreadMessagesRequest : IRequest<CountUnreadMessagesResponse>
    {
        [Required]
        public string UserId { get; set; }

    }
}