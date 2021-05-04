using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Story;

namespace MyStagram.Core.Logic.Requests.Command.Story
{
    public class WatchStoryRequest : IRequest<WatchStoryResponse>
    {
        [Required]
        public string StoryId { get; set; }

    }
}