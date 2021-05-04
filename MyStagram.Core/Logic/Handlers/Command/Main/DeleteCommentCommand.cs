using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Main;
using MyStagram.Core.Logic.Responses.Command.Main;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Main
{
    public class DeleteCommentCommand : IRequestHandler<DeleteCommentRequest, DeleteCommentResponse>
    {
        private readonly IMainService mainService;
        public DeleteCommentCommand(IMainService mainService)
        {
            this.mainService = mainService;

        }
        public async Task<DeleteCommentResponse> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
           => await mainService.DeleteComment(request.CommentId)
            ? new DeleteCommentResponse()
            : new DeleteCommentResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Comment cannot be deleted"
            ));

    }
}