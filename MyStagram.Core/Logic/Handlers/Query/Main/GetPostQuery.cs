using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Query.Main;
using MyStagram.Core.Logic.Responses.Query.Main;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Dtos.Main;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Main
{
    public class GetPostQuery : IRequestHandler<GetPostRequest, GetPostResponse>
    {
        private readonly IReadOnlyMainService mainService;
        private readonly IMapper mapper;
        public GetPostQuery(IReadOnlyMainService mainService, IMapper mapper)
        {
            this.mainService = mainService;
            this.mapper = mapper;

        }
        public async Task<GetPostResponse> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            Post post = default(Post);

            post = await mainService.GetPost(request.PostId);

            return post != null
            ? new GetPostResponse { Post = mapper.Map<PostDto>(post) }
            : new GetPostResponse(Error.Build
            (
                ErrorCodes.EntityNotFound,
                "Post cannot be found"
            ));
        }
    }
}