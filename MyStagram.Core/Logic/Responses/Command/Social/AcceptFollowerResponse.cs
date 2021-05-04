using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Social
{
    public class AcceptFollowerResponse : BaseResponse
    {
        public AcceptFollowerResponse(Error error = null) : base(error) { }
    }
}