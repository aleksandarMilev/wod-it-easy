namespace WodItEasy.Common.Application.Models
{
    using System.Collections.Generic;

    public class PaginatedOutputModel<T>(
        IEnumerable<T> items,
        int totalItems,
        int pageIndex,
        int pageSize)
    {
        public IEnumerable<T> Items { get; } = items;

        public int TotalItems { get; } = totalItems;

        public int PageIndex { get; } = pageIndex;

        public int PageSize { get; } = pageSize;
    }
}
