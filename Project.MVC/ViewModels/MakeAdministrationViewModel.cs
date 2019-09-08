using Project.MVC.Infrastructure;
using Project.MVC.Models;
using Project.Service;
using Project.Service.Interfaces;
using System.Collections.Generic;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za sortiranje, pretraživanje i oznaku stranica VehicleMake klase 
    /// </summary>
    public class MakeAdministrationViewModel
    {
        
        public IEnumerable<Make> VehicleMakers { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchString { get; set; }
        public string SortBy { get; set; }
    }
}
