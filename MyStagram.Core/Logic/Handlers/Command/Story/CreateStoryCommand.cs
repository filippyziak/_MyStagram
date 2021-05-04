using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Story;
using MyStagram.Core.Logic.Responses.Command.Story;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Dtos.Story;

namespace MyStagram.Core.Logic.Handlers.Command.Story
{
    public class CreateStoryCommand : IRequestHandler<CreateStoryRequest, CreateStoryResponse>
    {
        private readonly IMapper mapper;
        private readonly IStoryService storyService;

        public CreateStoryCommand(IMapper mapper, IStoryService storyService)
        {
            this.mapper = mapper;
            this.storyService = storyService;
        }
        public async Task<CreateStoryResponse> Handle(CreateStoryRequest request, CancellationToken cancellationToken)
        {
            var addedStory = await storyService.CreateStory(request.Photo);

            return addedStory != null ? new CreateStoryResponse { Story = mapper.Map<StoryDto>(addedStory) }
            : new CreateStoryResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Story creating fail"
            ));
        }
    }
}