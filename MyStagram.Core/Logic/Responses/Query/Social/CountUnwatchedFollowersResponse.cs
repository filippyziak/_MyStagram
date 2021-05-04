using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Query.Social
{
    public class CountUnwatchedFollowersResponse : BaseResponse
    {
        public int UnwatchedFollowsCount { get; set; }
        public CountUnwatchedFollowersResponse(Error error = null) : base(error) { }
    }
}