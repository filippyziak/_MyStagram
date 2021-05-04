using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Logic.Responses.Command.Profile;
using MyStagram.Core.Validators;

namespace MyStagram.Core.Logic.Requests.Command.Profile
{
    public class ChangeAvatarRequest : IRequest<ChangeAvatarResponse>
    {
        [Required]
        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        public IFormFile Avatar { get; set; }
    }
}