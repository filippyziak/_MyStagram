using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Query.Profile;

namespace MyStagram.Core.Logic.Requests.Query.Profile
{
    public class GetProfileRequest : IRequest<GetProfileResponse>
    {
        [Required]
        public string UserId { get; set; }
        
        
    }
}