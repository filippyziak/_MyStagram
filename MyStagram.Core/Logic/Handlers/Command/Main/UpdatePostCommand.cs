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
    public class UpdatePostCommand : IRequestHandler<UpdatePostRequest, UpdatePostResponse>
    {
        private readonly IMainService mainService;
        private readonly IMapper mapper;
        public UpdatePostCommand(IMainService mainService, IMapper mapper)
        {
            this.mapper = mapper;
            this.mainService = mainService;

        }
        public async Task<UpdatePostResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var post = await mainService.GetPost(request.PostId);
            post = mapper.Map<UpdatePostRequest, Post>(request, post);

            var updatedPost = await mainService.UpdatePost(post);

            return updatedPost != null ? new UpdatePostResponse { Post = mapper.Map<PostDto>(updatedPost) }
            : new UpdatePostResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Post has not been updated"
            ));
        }
    }
}