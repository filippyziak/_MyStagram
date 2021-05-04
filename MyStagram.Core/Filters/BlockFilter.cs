using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using MyStagram.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Data;

namespace MyStagram.Core.Filters
{
    public class BlockFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext result;

            string currentUserId = context.HttpContext.GetCurrentUserId();

            if (currentUserId == null)
            {
                result = await next();
                return;
            }

            var database = context.HttpContext.RequestServices.GetService<IDatabase>();

            var currentUser = await database.UserRepository.Get(currentUserId) ?? throw new EntityNotFoundException("User not found");

            if (currentUser.IsBlocked)
                throw new BlockException("Your account is blocked :)");

            result = await next();
        }
    }
}