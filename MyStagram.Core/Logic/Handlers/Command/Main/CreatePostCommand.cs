using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Main;
using MyStagram.Core.Logic.Responses.Command.Main;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Handlers.Command.Main
{
    public class CreatePostCommand : IRequestHandler<CreatePostRequest, CreatePostResponse>
    {
        private readonly IMainService mainService;
        private readonly IMapper mapper;
        public CreatePostCommand(IMainService mainService, IMapper mapper)
        {
            this.mapper = mapper;
            this.mainService = mainService;

        }
        public async Task<CreatePostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var post = await mainService.CreatePost(mapper.Map<Post>(request), photo: request.Photo);

            return post != null
            ? new CreatePostResponse { Post = mapper.Map<PostDto>(post) }
            : new CreatePostResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Post has not been created"
            ));
        }
    }
}