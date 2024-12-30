namespace WodItEasy.Application.Common
{
    using System.Collections.Generic;

    public class PaginatedOutputModel<T>
    {
        public PaginatedOutputModel(IEnumerable<T> items, int totalItems, int pageSize, int pageIndex)
        {
            this.Items = items;
            this.TotalItems = totalItems;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }

        public IEnumerable<T> Items { get; set; } = null!;

        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
