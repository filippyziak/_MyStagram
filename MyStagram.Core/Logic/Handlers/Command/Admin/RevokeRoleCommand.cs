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
    public class RevokeRoleCommand : IRequestHandler<RevokeRoleRequest, RevokeRoleResponse>
    {
        private readonly IAdminService adminService;
        public RevokeRoleCommand(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        public async Task<RevokeRoleResponse> Handle(RevokeRoleRequest request, CancellationToken cancellationToken)
         => await adminService.RevokeRole(request.UserId, request.RoleId)
        ? new RevokeRoleResponse()
        : new RevokeRoleResponse(Error.Build
        (
            ErrorCodes.AdminActionFailed,
            "You cannot revoke role of this user"
        ));
    }
}