using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Story;

namespace MyStagram.Core.Logic.Requests.Query.Story
{
    public class GetThisUserStoriesRequest : IRequest<GetThisUserStoriesResponse>
    {
        [Required]
        public string UserId { get; set; }
    }
}