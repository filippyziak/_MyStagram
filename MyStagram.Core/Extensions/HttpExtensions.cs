using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Models.Helpers.Pagination;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyStagram.Core.Extensions
{
    public static class HttpExtensions
    {
        public static string GetCurrentUserId(this HttpContext httpContext)
            => httpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null ? httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value : null;
    
        public static void AddPagination(this HttpResponse response, int currentPage, int pageSize, int totalCount, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, pageSize, totalCount, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();

            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination",
                JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}