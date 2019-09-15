using Project.Models.Interfaces;
using Project.MVC.Infrastructure;
using Project.Service;
using Project.Service.Interfaces;
using Project.Shared;
using System.Collections.Generic;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za sortiranje, pretraživanje i oznaku stranica VehicleMake klase 
    /// </summary>
    public class MakeAdministrationViewModel
    {
        
        public IEnumerable<IMake> VehicleMakers { get; set; }
        public IPaging PagingInfo { get; set; }

        public ISort SortingInfo { get; set; }

        public IFilter FilterInfo { get; set; }
    }
}
