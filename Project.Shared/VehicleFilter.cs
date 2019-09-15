using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Shared
{
    public class VehicleFilter : IFilter
    {
        public string Filter { get; set; }
        public string SearchString { get; set; }
    }
}
