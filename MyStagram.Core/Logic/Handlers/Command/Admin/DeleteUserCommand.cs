using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Admin;
using MyStagram.Core.Logic.Responses.Command.Admin;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Admin
{
    public class DeleteUserCommand : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IAdminService adminService;
        public DeleteUserCommand(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
         => await adminService.DeleteUser(request.UserId)
        ? new DeleteUserResponse()
        : new DeleteUserResponse(Error.Build
        (
            ErrorCodes.AdminActionFailed,
            "You cannot revoke role of this user"
        ));
    }
}