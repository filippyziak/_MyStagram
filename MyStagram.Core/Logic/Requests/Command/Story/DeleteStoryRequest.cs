using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Story;

namespace MyStagram.Core.Logic.Requests.Command.Story
{
    public class DeleteStoryRequest : IRequest<DeleteStoryResponse>
    {
        [Required]
        public string StoryId { get; set; }
        
        
    }
}