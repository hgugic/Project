using Microsoft.AspNetCore.Mvc;
using Project.MVC.Extensions;
using Project.MVC.Infrastructure;
using Project.MVC.Models;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Interfaces;

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

            viewModel.SearchString = searchString;
            viewModel.SortBy = sortBy;

            viewModel.VehicleMakers = vehicleService.FindMake(out int totalPages, searchString, searchFilter, sortBy, PageSize, page).AsMake();

            viewModel.PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalPages = totalPages };

            return View(viewModel);
        }


        public IActionResult Delete(int makeId)
        {
            vehicleService.DeleteMake(makeId);
            return RedirectToAction("Administration", "Make", new { page = 1 });
        }
        public ViewResult Edit(int makeId)
        {
            return View(new Make(vehicleService.GetMakeById(makeId)));
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
