using Microsoft.AspNetCore.Mvc;
using Project.Service.Interfaces;
using System.Linq;

namespace Project.MVC.Components
{
    /// <summary>
    /// komponenta za ubacivanje proizvođača na lijevi dio stranice tj. navigacijski
    /// </summary>
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IMakeRepository makeRepository;

        public NavigationMenuViewComponent(IMakeRepository makeRepository)
        {
            this.makeRepository = makeRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedMake = RouteData?.Values["makeId"];
            return View(makeRepository.VehicleMakers.OrderBy(x =>x.Name));
        }
    }
}
