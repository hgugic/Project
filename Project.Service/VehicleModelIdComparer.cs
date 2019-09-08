using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service
{
    internal class VehicleModelIdComparer : IEqualityComparer<IVehicleModel>
    {
        public bool Equals(IVehicleModel x, IVehicleModel y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(IVehicleModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
