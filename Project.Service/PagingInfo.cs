using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service
{
    /// <summary>
    /// Klasa za formatiranje stranica
    /// </summary>
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
