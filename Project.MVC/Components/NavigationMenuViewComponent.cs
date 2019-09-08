using Microsoft.AspNetCore.Mvc;
using Project.MVC.Extensions;
using Project.MVC.Models;
using Project.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Project.MVC.Components
{
    /// <summary>
    /// komponenta za ubacivanje proizvođača na lijevi dio stranice tj. navigacijski
    /// </summary>
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IVehicleService vehicleService;

        public NavigationMenuViewComponent(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public IViewComponentResult Invoke()
        {            
            ViewBag.SelectedMake = RouteData?.Values["makeId"];    
            return View(vehicleService.FindMake(out int totalPages).AsMake());
        }
    }
}
