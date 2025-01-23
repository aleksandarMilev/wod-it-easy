namespace WodItEasy.Application.Common
{
    using System.Collections.Generic;

    public class PaginatedOutputModel<T>
    {
        public PaginatedOutputModel(
            IEnumerable<T> items, 
            int totalItems, 
            int pageIndex, 
            int pageSize)
        {
            this.Items = items;
            this.TotalItems = totalItems;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public IEnumerable<T> Items { get; } = null!;

        public int TotalItems { get; }

        public int PageIndex { get; }

        public int PageSize { get; }
    }
}
