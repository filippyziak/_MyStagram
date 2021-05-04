using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Auth;

namespace MyStagram.Core.Logic.Requests.Query.Auth
{
    public class ConfirmAccountRequest : IRequest<ConfirmAccountResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}