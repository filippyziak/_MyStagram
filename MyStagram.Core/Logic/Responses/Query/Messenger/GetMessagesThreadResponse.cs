using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Messenger;
using MyStagram.Core.Models.Dtos.Social;

namespace MyStagram.Core.Logic.Responses.Query.Messenger
{
    public class GetMessagesThreadResponse : BaseResponse
    {
        public List<MessageDto> Messages { get; set; }
        public RecipientDto Recipient { get; set; }
        public GetMessagesThreadResponse(Error error = null) : base(error) { }
    }
}