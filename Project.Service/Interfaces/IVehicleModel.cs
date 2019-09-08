using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interfaces
{
    public interface IVehicleModel : IVehicleMake
    {
        int MakeId { get; set; }
    }
}
