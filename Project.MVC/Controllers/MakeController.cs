using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.MVC.Extensions;
using Project.MVC.Infrastructure;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Interfaces;
using Project.Shared;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {

        public int PageSize = 3;
        private readonly IVehicleService vehicleService;

        public MakeController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public ViewResult Administration(string searchString, string searchFilter, string sortBy, int page = 1)
        {
            MakeAdministrationViewModel viewModel = new MakeAdministrationViewModel();
            viewModel.FilterInfo = new VehicleFilter() { Filter = searchFilter, SearchString = searchString };
            viewModel.SortingInfo = new Sorting(sortBy);
            viewModel.PagingInfo = new Paging() { CurrentPage = page, ItemsPerPage = PageSize };
            viewModel.VehicleMakers = vehicleService.FindMake(viewModel.FilterInfo, viewModel.SortingInfo, viewModel.PagingInfo);

            return View(viewModel);
        }


        public IActionResult Delete(int makeId)
        {
            vehicleService.DeleteMake(makeId);
            return RedirectToAction("Administration", "Make", new { page = 1 });
        }
        public ViewResult Edit(int makeId)
        {
            return View(vehicleService.GetMakeById(makeId));
        }


        [HttpPost]
        public IActionResult Edit(Make vehicleMakeEdit)
        {
            if (ModelState.IsValid)
            {
                vehicleService.SaveChanges(vehicleMakeEdit);
                return RedirectToAction("Administration", "Make", new { page = 1});
            }
            return View(vehicleMakeEdit);
        }

        public IActionResult Create() => View("Edit", new Make());
    }
}
