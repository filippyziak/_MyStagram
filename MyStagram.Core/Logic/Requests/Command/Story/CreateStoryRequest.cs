using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Logic.Responses.Command.Story;
using MyStagram.Core.Validators;

namespace MyStagram.Core.Logic.Requests.Command.Story
{
    public class CreateStoryRequest : IRequest<CreateStoryResponse>
    {
        [Required]
        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        public IFormFile Photo { get; set; }
    }
}