using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Responses.Command.Main;

namespace MyStagram.Core.Logic.Requests.Command.Main
{
    public class UpdatePostRequest : IRequest<UpdatePostResponse>
    {
        [Required]
       public string PostId { get; set; } 

        [Required]
        [StringLength(maximumLength: Constants.MaximumDescriptionLength)]
        public string Description { get; set; }
    }
}