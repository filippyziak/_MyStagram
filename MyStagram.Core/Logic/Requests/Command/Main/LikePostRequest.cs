using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Main;

namespace MyStagram.Core.Logic.Requests.Command.Main
{
    public class LikePostRequest : IRequest<LikePostResponse>
    {
        [Required]
        public string PostId { get; set; }
    }
}