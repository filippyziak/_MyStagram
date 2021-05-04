using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Logic.Requests.Query.Story;
using MyStagram.Core.Logic.Responses.Query.Story;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Story
{
    public class GetStoriesQuery : IRequestHandler<GetStoriesRequest, GetStoriesResponse>
    {
        private readonly IReadOnlyStoryService storyService;
        public GetStoriesQuery(IReadOnlyStoryService storyService)
        {
            this.storyService = storyService;
        }
        public async Task<GetStoriesResponse> Handle(GetStoriesRequest request, CancellationToken cancellationToken)
        => new GetStoriesResponse { StoryWrappers = (await storyService.CreateStoryWrappers()).OrderBy(u => u.IsWatched).ThenBy(u => u.StoryToWatch.DateExpires) };
    }
}