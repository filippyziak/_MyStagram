using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Responses.Command.Auth;
using MyStagram.Core.Validators;

namespace MyStagram.Core.Logic.Requests.Command.Auth
{
    public class RegisterRequest : IRequest<RegisterResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.MaxUserNameLength, MinimumLength = Constants.MinUserNameLength)]
        [WhitespacesNotAllowedValidator]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(maximumLength: Constants.MaxUserPasswordLength, MinimumLength = Constants.MinUserPasswordLength)]
        [WhitespacesNotAllowedValidator]
        public string Password { get; set; }
    }
}