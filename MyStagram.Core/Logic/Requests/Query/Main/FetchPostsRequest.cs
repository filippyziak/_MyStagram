using System.Collections.Generic;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Main;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Logic.Requests.Query.Main
{
    public class FetchPostsRequest : PaginationRequest, IRequest<FetchPostsResponse>
    {
    }
}