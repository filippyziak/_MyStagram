using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Responses.Command.Profile;
using MyStagram.Core.Validators;

namespace MyStagram.Core.Logic.Requests.Command.Profile
{
    public class UpdateProfileRequest : IRequest<UpdateProfileResponse>
    {
        [Required]
        [WhitespacesNotAllowedValidator]
        [StringLength(maximumLength: Constants.MaxUserNameLength, MinimumLength = Constants.MinUserNameLength)]
        public string UserName { get; set; }

        
        [WhitespacesNotAllowedValidator]
        [StringLength(maximumLength: Constants.MaxUserNameLength)]
        public string Surname { get; set; }

          
        [WhitespacesNotAllowedValidator]
        [StringLength(maximumLength: Constants.MaxUserNameLength)]
        public string Name { get; set; }

        [StringLength(maximumLength: Constants.MaximumDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool IsPrivate { get; set; }
        
        
    }
}