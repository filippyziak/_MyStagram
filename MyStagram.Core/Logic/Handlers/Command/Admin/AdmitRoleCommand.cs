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
    public class AdmitRoleCommand : IRequestHandler<AdmitRoleRequest, AdmitRoleResponse>
    {
        private readonly IAdminService adminService;
        public AdmitRoleCommand(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        public async Task<AdmitRoleResponse> Handle(AdmitRoleRequest request, CancellationToken cancellationToken)
        => await adminService.AdmitRole(request.UserId, request.RoleId)
        ? new AdmitRoleResponse()
        : new AdmitRoleResponse(Error.Build
        (
            ErrorCodes.AdminActionFailed,
            "You cannot admit role to this user"
        ));
    }
}