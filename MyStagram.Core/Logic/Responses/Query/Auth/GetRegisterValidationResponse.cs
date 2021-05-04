using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Query.Auth
{
    public class GetRegisterValidationResponse : BaseResponse
    {
        public bool IsAvailable { get; set; }
        public GetRegisterValidationResponse(Error error = null) : base(error) { }
    }
}