using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Query.Profile;
using MyStagram.Core.Logic.Responses.Query.Profile;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Dtos.Profile;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Profile
{
    public class GetProfileQuery : IRequestHandler<GetProfileRequest, GetProfileResponse>
    {
        private readonly IReadOnlyProfileService profileService;
        private readonly IMapper mapper;
        public GetProfileQuery(IReadOnlyProfileService profileService, IMapper mapper)
        {
            this.profileService = profileService;
            this.mapper = mapper;

        }
        public async Task<GetProfileResponse> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            UserProfileDto userProfile = default(UserProfileDto);

            userProfile = mapper.Map<UserProfileDto>(await profileService.GetUser(request.UserId));

            return userProfile != null
            ? new GetProfileResponse { UserProfile = userProfile }
            : new GetProfileResponse(Error.Build
            (
                ErrorCodes.EntityNotFound,
                "User profile cannot be found"
            ));
        }
    }
}