using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Responses.Command.Main;

namespace MyStagram.Core.Logic.Requests.Command.Main
{
    public class CreateCommentRequest : IRequest<CreateCommentResponse>
    {
        [Required]
        [StringLength(maximumLength: Constants.MaximumCommentLength)]
        public string Content { get; set; }
        [Required]
        public string PostId { get; set; }
    }
}