using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Story;

namespace MyStagram.Core.Logic.Responses.Command.Story
{
    public class CreateStoryResponse : BaseResponse
    {
        public StoryDto Story { get; set; }

        public CreateStoryResponse(Error error = null) : base(error) { }
    }
}