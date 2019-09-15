using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.MVC.Extensions;
using Project.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Extensions;
using Project.MVC.Infrastructure;
using Project.Shared;
using Project.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.MVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleService vehicleService;
        public int PageSize = 3;
        public ModelController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }
        public ViewResult Administration(int? makeId, string searchString, string searchFilter, string sortBy,  int page = 1)
        {
            ModelAdministrationViewModel viewModel = new ModelAdministrationViewModel(searchString, searchFilter, sortBy);

            var sorting = new Sorting(sortBy);
            viewModel.PagingInfo = new Paging() { CurrentPage = page, ItemsPerPage = PageSize };

            if (makeId != null)
            {
                viewModel.CurrentMakeId = makeId;
                var filtering = new VehicleFilter() { Filter = "MakeId", SearchString = makeId.ToString() };
                viewModel.VehicleModels = vehicleService.FindModel(filtering, sorting, viewModel.PagingInfo);
                viewModel.VehicleModels = viewModel.VehicleModels.IncludeMake(vehicleService.FindMake(null, null, null));
            }
            else
            {

                var filtering = new VehicleFilter() { Filter = searchFilter, SearchString = searchString };
                viewModel.VehicleModels = vehicleService.FindModel(filtering, sorting, viewModel.PagingInfo);
                viewModel.VehicleModels = viewModel.VehicleModels.IncludeMake(vehicleService.FindMake(null, null, null));
            }

            return View(viewModel);
        }


        public IActionResult Delete(int modelId)
        {
            vehicleService.DeleteModel(modelId);
            return RedirectToAction("Administration", new { page = 1 });
        }


        public ViewResult Edit(int modelId)
        {

            var modelEdit = new ModelEditViewModel()
            {
                VehicleModel = vehicleService.GetModelById(modelId)
            };

            modelEdit.SelectList(vehicleService);

            return View(modelEdit);
        }


        [HttpPost]
        public IActionResult Edit(ModelEditViewModel vehicleModelEdit)
        {
            if (ModelState.IsValid)
            {
                vehicleService.SaveChanges(vehicleModelEdit.VehicleModel);
                TempData["message"] = $"{vehicleModelEdit.VehicleModel.Name} je spremljen";
                return RedirectToAction("Administration", new { page = 1,
                                                                searchFilter = "Name",
                                                                searchString = vehicleModelEdit.VehicleModel.Name.ToString()});
            }

            vehicleModelEdit.SelectList(vehicleService);
            return View(vehicleModelEdit);
        }

        public IActionResult Create()
        {
            ModelEditViewModel vm = new ModelEditViewModel() { VehicleModel = new Model()};
            vm.SelectList(vehicleService);
            return View("Edit", vm);
        }
                                                     
    }
}
