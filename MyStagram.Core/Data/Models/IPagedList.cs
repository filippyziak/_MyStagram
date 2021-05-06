using System.Collections.Generic;

namespace MyStagram.Core.Data.Models
{
    public interface IPagedList<T> : IEnumerable<T>
    {
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
    }
}