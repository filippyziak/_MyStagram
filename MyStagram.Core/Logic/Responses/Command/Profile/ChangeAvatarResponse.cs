using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Profile
{
    public class ChangeAvatarResponse : BaseResponse
    {
        public string Url { get; set; }
        
        public ChangeAvatarResponse(Error error = null) : base(error) { }
    }
}