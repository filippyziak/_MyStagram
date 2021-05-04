using System.Threading.Tasks;
using MyStagram.Core.Logic.Requests.Query.Profile;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Models.Helpers.Pagination;

namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyProfileService
    {
        Task<User> GetCurrentUser();
        Task<User> GetUser(string userId);
        Task<PagedList<User>> GetProfiles(GetProfilesRequest request);
    }
}