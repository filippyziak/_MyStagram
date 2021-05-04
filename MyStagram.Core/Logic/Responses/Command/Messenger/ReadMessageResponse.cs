using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Messenger
{
    public class ReadMessageResponse : BaseResponse
    {
        public ReadMessageResponse(Error error = null) : base(error) { }
    }
}