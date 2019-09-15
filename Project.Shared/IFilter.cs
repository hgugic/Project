using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Shared
{
    public interface IFilter
    {
        string Filter { get; set; }
        string SearchString { get; set; }
    }
}
