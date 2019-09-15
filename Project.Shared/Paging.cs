using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Shared
{
    public class Paging : IPaging
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
