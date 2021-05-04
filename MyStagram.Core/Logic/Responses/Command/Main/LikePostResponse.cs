using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Responses.Command.Main
{
    public class LikePostResponse : BaseResponse
    {
        public bool Result { get; set; }
        public LikeDto Like { get; set; }
        public LikePostResponse(Error error = null) : base(error) { }
    }
}