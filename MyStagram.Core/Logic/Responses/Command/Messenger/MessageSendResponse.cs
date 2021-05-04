using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Social;

namespace MyStagram.Core.Logic.Responses.Command.Messenger
{
    public class MessageSendResponse : BaseResponse
    {
        public MessageDto Message { get; set; }

        public MessageSendResponse(Error error = null) : base(error) { }
    }
}