using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Logic.Requests.Query.Story;
using MyStagram.Core.Logic.Responses.Query.Story;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Story
{
    public class GetThisUserStoriesQuery : IRequestHandler<GetThisUserStoriesRequest, GetThisUserStoriesResponse>
    {
        private readonly IReadOnlyStoryService storyService;
        public GetThisUserStoriesQuery(IReadOnlyStoryService storyService)
        {
            this.storyService = storyService;
        }
        public async Task<GetThisUserStoriesResponse> Handle(GetThisUserStoriesRequest request, CancellationToken cancellationToken)
        {
            var result = await storyService.GetStories(request.UserId);

            return new GetThisUserStoriesResponse
            {
                Stories = result.Stories,
                StoryToWatch = result.StoryToWatch
            };
        }
    }
}