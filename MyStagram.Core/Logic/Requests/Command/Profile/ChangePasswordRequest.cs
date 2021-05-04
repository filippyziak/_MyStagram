using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Validators;
using MyStagram.Core.Logic.Responses.Command.Profile;
using MyStagram.Core.Helpers;

namespace MyStagram.Core.Logic.Requests.Command.Profile
{
    public class ChangePasswordRequest : IRequest<ChangePasswordResponse>
    {
        [Required]

        public string oldPassword { get; set; }
        
        [Required]
        [WhitespacesNotAllowedValidator]
        [StringLength(maximumLength: Constants.MaxUserPasswordLength, MinimumLength = Constants.MinUserPasswordLength)]
        public string newPassword { get; set; }
        
        
    
    }
}