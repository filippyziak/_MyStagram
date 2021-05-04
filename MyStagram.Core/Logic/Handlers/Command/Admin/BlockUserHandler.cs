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
    public class BlockUserHandler : IRequestHandler<BlockUserRequest, BlockUserResponse>
    {
        private readonly IAdminService adminService;

        public BlockUserHandler(IAdminService adminService)
        {
            this.adminService = adminService;

        }
        public async Task<BlockUserResponse> Handle(BlockUserRequest request, CancellationToken cancellationToken)
        {
            return new BlockUserResponse { IsBlocked = await adminService.BlockUser(request.UserId) };
        }
    }
}