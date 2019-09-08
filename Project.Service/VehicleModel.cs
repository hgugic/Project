using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project.Service
{
    /// <summary>
    /// Klasa predstavlja modela vozila sa vezom na proizvođača
    /// </summary>
    public class VehicleModel : Vehicle, IVehicleModel
    {
        public int MakeId { get; set; }

        public VehicleMake Make { get; set; }
    }
}