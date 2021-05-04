using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Main;
using MyStagram.Core.Logic.Responses.Query.Main;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Dtos.Main;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Main
{
    public class GetPostsQuery : IRequestHandler<GetPostsRequest, GetPostsResponse>
    {
        private readonly IReadOnlyMainService mainService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public GetPostsQuery(IReadOnlyMainService mainService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.mainService = mainService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

        }
        public async Task<GetPostsResponse> Handle(GetPostsRequest request, CancellationToken cancellationToken)
        {
            var posts = await mainService.GetPosts(request);

            httpContextAccessor.HttpContext.Response.AddPagination(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages);

            return new GetPostsResponse { Posts = mapper.Map<List<PostsDto>>(posts) };
        }
    }
}