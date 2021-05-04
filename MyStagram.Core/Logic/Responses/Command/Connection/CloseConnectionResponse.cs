using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Connection
{
    public class CloseConnectionResponse : BaseResponse
    {
        public CloseConnectionResponse(Error error = null) : base(error) { }
    }
}