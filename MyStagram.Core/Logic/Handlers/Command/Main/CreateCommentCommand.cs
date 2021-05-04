using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Main;
using MyStagram.Core.Logic.Responses.Command.Main;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Handlers.Command.Main
{
    public class CreateCommentCommand : IRequestHandler<CreateCommentRequest, CreateCommentResponse>
    {
        private readonly IMainService mainService;
        private readonly IMapper mapper;
        public CreateCommentCommand(IMainService mainService, IMapper mapper)
        {
            this.mainService = mainService;
            this.mapper = mapper;

        }
        public async Task<CreateCommentResponse> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var createdComment = await mainService.CreateComment(request.PostId, request.Content);

            return createdComment != null 
            ? new CreateCommentResponse {Comment = mapper.Map<CommentDto>(createdComment)}
            : new CreateCommentResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Comment has not been created"
            ));

        }
    }
}