using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses
{
    public interface IBaseResponse
    {
        bool IsSucceeded { get; }

        Error Error { get; }
    }
}