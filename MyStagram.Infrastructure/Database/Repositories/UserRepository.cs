using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Data.Repositories;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Profile;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Infrastructure.Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<User>> GetProfiles(GetProfilesRequest request)
        => string.IsNullOrEmpty(request.UserName)
        ? await context.Users
        .OrderByDescending(u => u.Created)
        .ToPagedList<User>(request.PageNumber, request.PageSize)
        
        : await context.Users
        .Where(u => u.UserName.ToLower().Contains(request.UserName.ToLower()))
        .OrderByDescending(u => u.Created)
        .ToPagedList<User>(request.PageNumber, request.PageSize);
    }
}