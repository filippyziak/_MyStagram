using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Social
{
    public class WatchFollowerResponse : BaseResponse
    {
        public WatchFollowerResponse(Error error = null) : base(error) { }
    }
}