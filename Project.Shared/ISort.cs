using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Shared
{
    public interface ISort
    {
        string SortBy { get; set; }
        string SortDirection { get; set; }
    }
}
