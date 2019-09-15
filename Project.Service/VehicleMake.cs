using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Service
{
    /// <summary>
    /// Klasa predstavlja proizvođača vozila sa vezom na modele
    /// </summary>
    public class VehicleMake : Vehicle, IVehicleMake
    {
        public ICollection<VehicleModel> VehicleModels { get; set; }

    }
}