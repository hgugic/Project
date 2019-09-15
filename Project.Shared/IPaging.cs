using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Shared
{
    public interface IPaging
    {
        int ItemsPerPage { get; set; }
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        int PageSize { get; set; }
    }
}
