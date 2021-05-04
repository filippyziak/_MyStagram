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
    public class ChangeAvatarCommand : IRequestHandler<ChangeAvatarRequest, ChangeAvatarResponse>
    {
        private readonly IProfileService profileService;
        public ChangeAvatarCommand(IProfileService profileService)
        {
            this.profileService = profileService;

        }
        public async Task<ChangeAvatarResponse> Handle(ChangeAvatarRequest request, CancellationToken cancellationToken)
        {
            var url = await profileService.SetAvatar(request.Avatar);

            return !string.IsNullOrEmpty(url)
            ? new ChangeAvatarResponse { Url = url }
            : new ChangeAvatarResponse(Error.Build
            (
                ErrorCodes.UploadPhotoFailed,
                "Error occured during changing the avatar"
            ));
        }
    }
}