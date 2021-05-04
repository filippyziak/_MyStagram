using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Main
{
    public class DeletePostResponse : BaseResponse
    {
        public DeletePostResponse(Error error = null) : base(error) { }
    }
}