using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Query.Messenger
{
    public class CountUnreadMessagesResponse : BaseResponse
    {
        public int UnreadMessagesCount { get; set; } 
        public CountUnreadMessagesResponse(Error error = null) : base(error) { }
    }
}