using Project.Service;
using System.Collections.Generic;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za sortiranje, pretraživanje i oznaku stranica VehicleMake klase 
    /// </summary>
    public class MakeAdministrationViewModel
    {
        public IEnumerable<VehicleMake> VehicleMakers { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
        public string SearchString { get; set; }
    }
}
