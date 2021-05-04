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
    public class FetchPostsQuery : IRequestHandler<FetchPostsRequest, FetchPostsResponse>
    {
        private readonly IMapper mapper;
        private readonly IReadOnlyMainService mainService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public FetchPostsQuery(IMapper mapper, IReadOnlyMainService mainService, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.mainService = mainService;
            this.httpContextAccessor = httpContextAccessor;

        }
        public async Task<FetchPostsResponse> Handle(FetchPostsRequest request, CancellationToken cancellationToken)
        {
            var posts = await mainService.FetchPosts(request);
            httpContextAccessor.HttpContext.Response.AddPagination(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages);

            return new FetchPostsResponse { Posts = mapper.Map<List<PostDto>>(posts) };
        }
    }
}