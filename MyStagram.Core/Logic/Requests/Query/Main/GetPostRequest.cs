using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Main;

namespace MyStagram.Core.Logic.Requests.Query.Main
{
    public class GetPostRequest : IRequest<GetPostResponse>
    {
        [Required]
       public string PostId { get; set; } 
    }
}