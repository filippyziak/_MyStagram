using MediatR;
using MyStagram.Core.Logic.Responses.Query.Profile;

namespace MyStagram.Core.Logic.Requests.Query.Profile
{
    public class GetProfilesRequest : PaginationRequest, IRequest<GetProfilesResponse>
    {
        public string UserName { get; set; }

        public GetProfilesRequest()
        {
            PageSize = 15;
        }
    }
}