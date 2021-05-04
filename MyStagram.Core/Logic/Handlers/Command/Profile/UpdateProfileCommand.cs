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
    public class UpdateProfileCommand : IRequestHandler<UpdateProfileRequest, UpdateProfileResponse>
    {
        private readonly IProfileService profileService;
        private readonly IAuthService authService;
        public UpdateProfileCommand(IProfileService profileService, IAuthService authService)
        {
            this.profileService = profileService;
            this.authService = authService;

        }
        public async Task<UpdateProfileResponse> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            var user = await profileService.GetCurrentUser();

            if ( request.Email != user.Email && await authService.EmailExists(request.Email))
                return new UpdateProfileResponse(Error.Build
                (
                    ErrorCodes.EmailExists,
                    "Email already in use"
                ));

            if (request.UserName != user.UserName && await authService.UserNameExists(request.UserName))
                return new UpdateProfileResponse(Error.Build
                (
                    ErrorCodes.UsernameExists,
                    "Username already in use"
                ));

            var updateProfileResult = await profileService.UpdateProfile(request.UserName, request.Surname, request.Name, request.Description, request.Email, request.IsPrivate);

            return updateProfileResult != null ? new UpdateProfileResponse { UpdateProfileResult = updateProfileResult }
                : new UpdateProfileResponse(Error.Build
                (
                    ErrorCodes.UpdateFailed,
                    "Error occurred during updating profile"
                ));
        } 
    }
}