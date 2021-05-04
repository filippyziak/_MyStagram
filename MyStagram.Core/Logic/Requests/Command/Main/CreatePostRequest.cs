using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Responses.Command.Main;
using MyStagram.Core.Validators;

namespace MyStagram.Core.Logic.Requests.Command.Main
{
    public class CreatePostRequest : IRequest<CreatePostResponse>
    {
        [Required]
        [StringLength(maximumLength: Constants.MaximumDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        public IFormFile Photo { get; set; }
    }
}