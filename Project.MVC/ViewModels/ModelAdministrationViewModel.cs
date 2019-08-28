using Project.Service;
using Project.Service.Enums;
using System.Collections.Generic;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za sortiranje, pretraživanje, filtriranje i oznaku stranica VehicleModel klase 
    /// </summary>
    public class ModelAdministrationViewModel
    {
        public IEnumerable<VehicleModel> VehicleModels { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public int? CurrentMakeId { get; set; }

        public SortingInfo SortingInfo { get; set; }

        public string SearchString { get; set; }

        public VehicleData SearchFilter { get; set; }
    }
}
