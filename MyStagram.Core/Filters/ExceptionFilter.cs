using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Extensions;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Responses;
using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var errorCode = ErrorCodes.ServerError;
            var errorMessage = context.Exception.Message;
            var statusCode = HttpStatusCode.InternalServerError;

            switch (context.Exception)
            {
                case AuthException _:
                    statusCode = HttpStatusCode.NotFound;
                    errorCode = (context.Exception as AuthException).ErrorCode;
                    break;
                case EntityNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    errorCode = (context.Exception as EntityNotFoundException).ErrorCode;
                    break;
                case NoPermissionsException _:
                    statusCode = HttpStatusCode.NotFound;
                    errorCode = (context.Exception as NoPermissionsException).ErrorCode;
                    break;
                default:
                    break;
            }

            var jsonResponse = (new BaseResponse(Error.Build(errorCode, errorMessage))).ToJSON();

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.HttpContext.Response.ContentLength = Encoding.UTF8.GetBytes(jsonResponse).Count();

            await context.HttpContext.Response.WriteAsync(jsonResponse);

            await base.OnExceptionAsync(context);
        }
    }
}