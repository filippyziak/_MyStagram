using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Messenger;

namespace MyStagram.Core.Logic.Requests.Query.Messenger
{
    public class GetConversationsRequest : PaginationRequest, IRequest<GetConversationsResponse>
    {
        public string UserName { get; set; }
    }
}