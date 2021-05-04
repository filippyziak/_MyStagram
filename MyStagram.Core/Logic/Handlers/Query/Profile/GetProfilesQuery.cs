using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Profile;
using MyStagram.Core.Logic.Responses.Query.Profile;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Dtos.Profile;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Profile
{
    public class GetProfilesQuery : IRequestHandler<GetProfilesRequest, GetProfilesResponse>
    {
        private readonly IMapper mapper;
        private readonly IReadOnlyProfileService profileService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public GetProfilesQuery(IReadOnlyProfileService profileService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.profileService = profileService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<GetProfilesResponse> Handle(GetProfilesRequest request, CancellationToken cancellationToken)
        {
            var profiles = await profileService.GetProfiles(request);

            httpContextAccessor.HttpContext.Response.AddPagination(profiles.CurrentPage, profiles.PageSize, profiles.TotalCount, profiles.TotalPages);

            return new GetProfilesResponse { UserProfiles = mapper.Map<List<SearchUserDto>>(profiles) };
        }
    }
}