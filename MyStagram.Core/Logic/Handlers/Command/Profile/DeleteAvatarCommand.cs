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
    public class DeleteAvatarCommand : IRequestHandler<DeleteAvatarRequest, DeleteAvatarResponse>
    {
        private readonly IProfileService profileService;
        public DeleteAvatarCommand(IProfileService profileService)
        {
            this.profileService = profileService;

        }
        public async Task<DeleteAvatarResponse> Handle(DeleteAvatarRequest request, CancellationToken cancellationToken)
        {
            return await profileService.DeleteAvatar()
            ? new DeleteAvatarResponse()
            : new DeleteAvatarResponse(Error.Build
            (
                ErrorCodes.DeletePhotoFailed,
                "Error occured during deleting the avatar"
            ));
        }
    }
}