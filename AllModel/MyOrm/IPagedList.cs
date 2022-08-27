﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AllModel.MyOrm
{
    public interface IPagedList<out T>
    {
        int Current { get; }

        int PageSize { get; }

        int Total { get; }

        int PageTotal { get; }

        IEnumerable<T> Item { get; }
    }
}
