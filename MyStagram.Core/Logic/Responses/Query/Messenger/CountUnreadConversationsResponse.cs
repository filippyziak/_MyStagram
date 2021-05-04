using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Query.Messenger
{
    public class CountUnreadConversationsResponse : BaseResponse
    {
        public int UnreadConversationsCount { get; set; }
        public CountUnreadConversationsResponse(Error error = null) : base(error) { }
    }
}