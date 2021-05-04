using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Helpers.Story;

namespace MyStagram.Core.Logic.Responses.Query.Story
{
    public class GetStoriesResponse : BaseResponse
    {
        public IEnumerable<StoryWrapper> StoryWrappers { get; set; }
        public GetStoriesResponse(Error error = null) : base(error) { }
    }
}