using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Social;
using MyStagram.Core.Logic.Responses.Query.Social;
using MyStagram.Core.Models.Dtos.Social;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Social
{
    public class GetFollowersQuery : IRequestHandler<GetFollowersRequest, GetFollowersResponse>
    {
        private readonly IReadOnlyFollowersService followersService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public GetFollowersQuery(IReadOnlyFollowersService followersService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.followersService = followersService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

        }
        public async Task<GetFollowersResponse> Handle(GetFollowersRequest request, CancellationToken cancellationToken)
        {
            var follows = await followersService.GetFollowers(request);

            var followers = follows.Followers;
            var following = follows.Following;

            httpContextAccessor.HttpContext.Response.AddPagination(followers.CurrentPage, followers.PageSize, followers.TotalCount, followers.TotalPages);

            return new GetFollowersResponse
            {
                Followers = mapper.Map<List<FollowerDto>>(followers),
                Following = mapper.Map<List<FollowerDto>>(following)
            };

        }
    }
}