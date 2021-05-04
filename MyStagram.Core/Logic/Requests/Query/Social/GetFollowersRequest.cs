using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Social;

namespace MyStagram.Core.Logic.Requests.Query.Social
{
    public class GetFollowersRequest : PaginationRequest, IRequest<GetFollowersResponse>
    {
        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool AreAccepted { get; set; }  
    }
}