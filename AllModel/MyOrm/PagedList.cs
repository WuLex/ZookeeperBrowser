using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllModel.MyOrm
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList()
        {
        }

        public PagedList(IList<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Current = pageIndex;
            PageSize = pageSize;
            Total = totalCount;
            PageTotal = (int)Math.Ceiling(totalCount / (double)pageSize);
            Item = items;
        }

        //internal PagedList() { }

        public int Current { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public int PageTotal { get; set; }

        public IEnumerable<T> Item { get; set; }

        public static PagedList<T> Create(IPagedList<T> source)
        {
            if (source is PagedList<T> same)
                return same;
            return new PagedList<T>(source.Item.ToList(), source.Current, source.PageSize, source.Total);
        }
    }
}