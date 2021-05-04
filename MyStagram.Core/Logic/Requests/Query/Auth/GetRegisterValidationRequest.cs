using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Enums;
using MyStagram.Core.Logic.Responses.Query.Auth;

namespace MyStagram.Core.Logic.Requests.Query.Auth
{
    public class GetRegisterValidationRequest : IRequest<GetRegisterValidationResponse>
    {
        [Required]
        public RegisterValidation RegisterValidation { get; set; }

        [Required]
        public string Content { get; set; }
    }
}