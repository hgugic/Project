using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Enums;
using Project.Service.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IMakeRepository makeRepository;
        public int PageSize = 3;

        public MakeController(IMakeRepository makeRepository)
        {
            this.makeRepository = makeRepository;
        }

        public ViewResult Administration(string searchString, VehicleData sort = default, bool sortByDescending = false, int page = 1)
        {
            MakeAdministrationViewModel viewModel = new MakeAdministrationViewModel();

            viewModel.SearchString = searchString;
            viewModel.VehicleMakers = makeRepository.Search(searchString, makeRepository.VehicleMakers);
            viewModel.VehicleMakers = makeRepository.SortBy(sort, sortByDescending, viewModel.VehicleMakers);


            int sizeOfCollection;

            sizeOfCollection = Pagination(page, viewModel);

            viewModel.SortingInfo = new SortingInfo() { SortBy = sort, SortByDescending = sortByDescending };

            viewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = sizeOfCollection
            };

            return View(viewModel);
        }
        /// <summary>
        /// Određivanje veličine kolekcije i priprema kolekcije za stranice
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns>int</returns>
        private int Pagination(int page, MakeAdministrationViewModel viewModel)
        {
            int sizeOfCollection;
            if (viewModel.VehicleMakers == null || !viewModel.VehicleMakers.Any())
            {
                sizeOfCollection = 0;
            }
            else
            {
                sizeOfCollection = viewModel.VehicleMakers.Count();
                viewModel.VehicleMakers = makeRepository.Paging(page, PageSize, viewModel.VehicleMakers);
            }

            return sizeOfCollection;
        }

        public IActionResult Delete(int makeId)
        {
            makeRepository.DeleteVehicleMake(makeId);
            return RedirectToAction("Administration", "Make", new { page = 1 });
        }
        public ViewResult Edit(int makeId)
        {
            return View(makeRepository.VehicleMakers.FirstOrDefault(x=>x.Id==makeId));
        }


        [HttpPost]
        public IActionResult Edit(VehicleMake vehicleMakeEdit)
        {
            if (ModelState.IsValid)
            {

                makeRepository.SaveVehicleMake(vehicleMakeEdit);
                return RedirectToAction("Administration", "Make", new { page = 1});
            }
            return View(vehicleMakeEdit);
        }

        public IActionResult Create() => View("Edit", new VehicleMake());
    }
}
