using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Profile;
using MyStagram.Core.Logic.Responses.Command.Profile;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Profile
{
    public class ChangePasswordCommand : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly IProfileService profileService;

        public ChangePasswordCommand(IProfileService profileService)
        {
            this.profileService = profileService;

        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var changePasswordResult =  await profileService.ChangePassword(request.oldPassword, request.newPassword);
            
            return changePasswordResult.HasChanged
            ? new ChangePasswordResponse()
            : new ChangePasswordResponse(Error.Build
            (
                ErrorCodes.ChangePasswordFailed,
                changePasswordResult.ErrorMessage
            ));
        }
    }
}