using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyStagram.Core.Logic.Responses;

namespace MyStagram.Core.Models.Helpers.Validation
{
    public class ValidationResultModel : BaseResponse
    {
        public List<ValidationError> ValidationErrors { get; }

        public ValidationResultModel(ModelStateDictionary modelState, MyStagram.Core.Models.Helpers.Error.Error error = null)
            : base(error)
        {
            ValidationErrors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(e => new ValidationError(key, e.ErrorMessage)))
                .ToList();
        }
    }
}