using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Profile
{
    public class DeleteAvatarResponse : BaseResponse
    {
        public DeleteAvatarResponse(Error error = null) : base(error) { }
        
    }
}