using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Story;
using MyStagram.Core.Logic.Responses.Command.Story;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Story
{
    public class WatchStoryCommand : IRequestHandler<WatchStoryRequest, WatchStoryResponse>
    {
        private readonly IStoryService storyService;
        public WatchStoryCommand(IStoryService storyService)
        {
            this.storyService = storyService;

        }
        public async Task<WatchStoryResponse> Handle(WatchStoryRequest request, CancellationToken cancellationToken)
        => await storyService.WatchStory(request.StoryId)
        ? new WatchStoryResponse()
        : new WatchStoryResponse(Error.Build
        (
            ErrorCodes.CrudActionFailed,
            "Cannot watch this story"
        ));
    }
}