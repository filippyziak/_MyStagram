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
    public class DeletePostCommand : IRequestHandler<DeletePostRequest, DeletePostResponse>
    {
        private readonly IMainService mainService;
        public DeletePostCommand(IMainService mainService)
        {
            this.mainService = mainService;
        }
        public async Task<DeletePostResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            return await mainService.DeletePost(request.PostId) 
            ? new DeletePostResponse()
            : new DeletePostResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Post cannot be deleted"
            ));
        }
    }
}