using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Responses.Command.Auth;
using MyStagram.Core.Validators;

namespace MyStagram.Core.Logic.Requests.Command.Auth
{
    public class ResetPasswordRequest : IRequest<ResetPasswordResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.MaxUserPasswordLength, MinimumLength = Constants.MinUserNameLength)]
        [WhitespacesNotAllowedValidator]
        public string NewPassword { get; set; }
        
    }
}