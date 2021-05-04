using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Profile;

namespace MyStagram.Core.Logic.Responses.Query.Profile
{
    public class GetProfileResponse : BaseResponse
    {
        public UserProfileDto UserProfile { get; set; }
        public GetProfileResponse(Error error = null) : base(error) { }
    }
}