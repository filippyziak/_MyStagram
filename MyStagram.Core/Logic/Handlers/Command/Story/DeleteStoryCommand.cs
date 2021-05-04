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
    public class DeleteStoryCommand : IRequestHandler<DeleteStoryRequest, DeleteStoryResponse>
    {
        private readonly IStoryService storyService;
        public DeleteStoryCommand(IStoryService storyService)
        {
            this.storyService = storyService;

        }
        public async Task<DeleteStoryResponse> Handle(DeleteStoryRequest request, CancellationToken cancellationToken)
        => await storyService.DeleteStory(request.StoryId)
        ? new DeleteStoryResponse()
        : new DeleteStoryResponse(Error.Build
        (
            ErrorCodes.CrudActionFailed,
            "Cannot delete this story"
        ));
    }
}