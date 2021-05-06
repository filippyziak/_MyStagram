using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Models.Helpers.Pagination;

namespace MyStagram.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> collection, int pageNumber, int pageSize) where T : class
                    =>await PagedList<T>.CreateAsync(collection, pageNumber, pageSize);
    }
}