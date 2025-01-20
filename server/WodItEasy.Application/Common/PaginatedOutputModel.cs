namespace WodItEasy.Application.Common
{
    using System.Collections.Generic;

    public class PaginatedOutputModel<T>
    {
        public PaginatedOutputModel(
            IEnumerable<T> items, 
            int totalItems, 
            int pageSize, 
            int pageIndex)
        {
            this.Items = items;
            this.TotalItems = totalItems;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }

        public IEnumerable<T> Items { get; } = null!;

        public int TotalItems { get; }

        public int PageSize { get; }

        public int PageIndex { get; }
    }
}
