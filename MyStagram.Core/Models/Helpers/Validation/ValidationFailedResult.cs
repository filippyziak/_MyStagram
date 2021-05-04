using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyStagram.Core.Models.Helpers.Validation
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState, MyStagram.Core.Models.Helpers.Error.Error error) : base(new ValidationResultModel(modelState, error))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}