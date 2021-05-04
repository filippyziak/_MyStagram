using MyStagram.Core.Models.Domain.Main;

namespace MyStagram.Core.Models.Helpers.Result
{
    public class LikeResult
    {

        public bool Result { get; }
        public Like Like { get; }
        public LikeResult(bool result = false, Like like = null)
        {
            Result = result;
            Like = like;
        }
    }
}