using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Messenger;

namespace MyStagram.Core.Logic.Requests.Query.Messenger
{
    public class GetMessagesThreadRequest : PaginationRequest, IRequest<GetMessagesThreadResponse>
    {
        [Required]
        public string RecipientId { get; set; }

    }
}