using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Connection
{
    public class StartConnectionResponse : BaseResponse
    {
        public StartConnectionResponse(Error error = null) : base(error) { }
    }
}