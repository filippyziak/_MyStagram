using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Main;
using MyStagram.Core.Logic.Responses.Command.Main;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Handlers.Command.Main
{
    public class LikePostCommand : IRequestHandler<LikePostRequest, LikePostResponse>
    {
        private readonly IMainService mainService;
        private readonly IMapper mapper;
        public LikePostCommand(IMainService mainService, IMapper mapper)
        {
            this.mainService = mainService;
            this.mapper = mapper;

        }
        public async Task<LikePostResponse> Handle(LikePostRequest request, CancellationToken cancellationToken)
        {
            var likeResult = await mainService.LikePost(request.PostId);

            var newLike = likeResult.Like != null ? mapper.Map<LikeDto>(likeResult.Like) : null;

            return likeResult != null 
            ? new LikePostResponse { Result = likeResult.Result, Like = newLike}
            : new LikePostResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Like cannot be changed"
            ));
        }
    }
}