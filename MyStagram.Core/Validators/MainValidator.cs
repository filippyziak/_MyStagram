using Microsoft.AspNetCore.Mvc.Filters;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Helpers.Validation;

namespace MyStagram.Core.Validators
{
    public class MainValidator : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new ValidationFailedResult(context.ModelState, Error.Build(ErrorCodes.ValidationError, "Invalid input data"));
        }
    }
}