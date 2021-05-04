using System.Collections.Generic;
using MyStagram.Core.Models.Dtos.Story;

namespace MyStagram.Core.Models.Helpers.Result
{
    public class GetStoriesResult
    {

        public List<StoryDto> Stories { get; }
        public StoryDto StoryToWatch { get; }

        public GetStoriesResult(List<StoryDto> stories, StoryDto storyToWatch)
        {
            Stories = stories;
            StoryToWatch = storyToWatch;
        }
    }
}