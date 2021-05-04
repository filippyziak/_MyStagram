using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Helpers.Result;

namespace MyStagram.Core.Logic.Responses.Command.Profile
{
    public class UpdateProfileResponse : BaseResponse
    {
        public UpdateProfileResult UpdateProfileResult { get; set; }
        public UpdateProfileResponse(Error error = null) : base(error) { }
    }
}