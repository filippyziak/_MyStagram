using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Helpers.Messenger;

namespace MyStagram.Core.Logic.Responses.Query.Messenger
{
    public class GetConversationsResponse : BaseResponse
    {
        public List<Conversation> Conversations { get; set; }
        public GetConversationsResponse(Error error = null) : base(error) { }
    }
}