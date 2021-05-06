using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Logic.Requests.Query.Profile;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IPagedList<User>> GetProfiles(GetProfilesRequest request);
    }
}