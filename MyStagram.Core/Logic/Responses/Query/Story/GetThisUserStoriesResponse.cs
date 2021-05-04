using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Story;

namespace MyStagram.Core.Logic.Responses.Query.Story
{
    public class GetThisUserStoriesResponse : BaseResponse
    {
        public List<StoryDto> Stories { get; set; }
        public StoryDto StoryToWatch { get; set; }
        public GetThisUserStoriesResponse(Error error = null) : base(error) { }
    }
}