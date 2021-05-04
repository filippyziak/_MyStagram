using MediatR;
using MyStagram.Core.Logic.Responses.Query.Main;

namespace MyStagram.Core.Logic.Requests.Query.Main
{
    public class GetPostsRequest : PaginationRequest, IRequest<GetPostsResponse>
    {
        public string UserId { get; set; }
    }
}