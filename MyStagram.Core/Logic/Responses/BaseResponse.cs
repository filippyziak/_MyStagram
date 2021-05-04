using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses
{
    public class BaseResponse
    {
        public bool IsSucceeded { get; }

        public Error Error { get; }

        public BaseResponse(Error error = null)
        {
            Error = error;

            IsSucceeded = Error == null;
        }
    }
}