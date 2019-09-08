using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Extensions;
using Project.MVC.Infrastructure;
using Project.MVC.Extensions;

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
            ModelAdministrationViewModel viewModel = new ModelAdministrationViewModel(vehicleService);

            viewModel.SearchString = searchString;
            viewModel.SearchFilter = searchFilter;
            viewModel.SortBy = sortBy;
            viewModel.CurrentMakeId = makeId;
            viewModel.PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize };

            int totalPages;

            if (makeId != null)
            {
                viewModel.VehicleModels = vehicleService.FindModel(out totalPages, makeId.ToString(), "MakeId", sortBy, PageSize, page)
                                                        .AsModel()
                                                        .IncludeMake(vehicleService.FindMake(out int i));
            }
            else
            {
                viewModel.VehicleModels = vehicleService.FindModel(out totalPages, searchString, searchFilter, sortBy, PageSize, page)
                                                        .AsModel()
                                                        .IncludeMake(vehicleService.FindMake(out int i));

            }
            viewModel.PagingInfo.TotalPages = totalPages;

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
                VehicleModel = new Models.Model(vehicleService.GetModelById(modelId))
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
            ModelEditViewModel vm = new ModelEditViewModel();
            vm.SelectList(vehicleService);
            return View("Edit", vm);
        }
                                                     
    }
}
