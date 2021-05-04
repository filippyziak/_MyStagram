using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Main;

namespace MyStagram.Core.Logic.Requests.Command.Main
{
    public class DeleteCommentRequest : IRequest<DeleteCommentResponse>
    {
        [Required]
        public string CommentId { get; set; }
    }
}