using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Profile;

namespace MyStagram.Core.Logic.Responses.Query.Profile
{
    public class GetProfilesResponse : BaseResponse
    {
        public List<SearchUserDto> UserProfiles { get; set; }

        public GetProfilesResponse(Error error = null) : base(error) { }
    }
}