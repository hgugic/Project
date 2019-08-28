using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Enums;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.MVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelRepository modelRepository;
        public int PageSize = 3;
        public ModelController(IModelRepository modelRepository)
        {
            this.modelRepository = modelRepository;
        }
        public ViewResult Administration(string searchString, int? makeId, VehicleData searchFilter = default, VehicleData sort = default, bool sortByDescending = false,  int page = 1)
        {
            ModelAdministrationViewModel viewModel = new ModelAdministrationViewModel();

            viewModel.SearchString = searchString;
            viewModel.SearchFilter = searchFilter;
            viewModel.SortingInfo = new SortingInfo() { SortBy = sort, SortByDescending = sortByDescending };

            viewModel.VehicleModels = modelRepository.Search(searchString, modelRepository.VehicleModels, searchFilter);
            viewModel.VehicleModels = modelRepository.SortBy(sort, sortByDescending, viewModel.VehicleModels);


            int sizeOfCollection;
            int sizeOfSelectedMakeCollection;

            if (viewModel.VehicleModels == null || !viewModel.VehicleModels.Any())
            {
                sizeOfCollection = 0;
                sizeOfSelectedMakeCollection = 0;
            }
            else
            {
                sizeOfCollection = viewModel.VehicleModels.Count();
                sizeOfSelectedMakeCollection = modelRepository.VehicleModels.Where(x => x.MakeId == makeId).Count();
                viewModel.VehicleModels = modelRepository.Paging(page, PageSize, makeId, viewModel.VehicleModels);
            }

            viewModel.PagingInfo = new PagingInfo() {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = makeId == null ? sizeOfCollection : sizeOfSelectedMakeCollection
            };

            viewModel.CurrentMakeId = makeId;
            
            return View(viewModel);
        }


        public IActionResult Delete(int modelId)
        {
            modelRepository.DeleteVehicleModel(modelId);
            return RedirectToAction("Administration", new { page = 1 });
        }


        public ViewResult Edit(int modelId)
        {
            var vehicleModel = modelRepository.VehicleModels.FirstOrDefault(x => x.Id == modelId);
            var modelEdit = new ModelEditViewModel
            {
                Id = vehicleModel.Id,
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                ListMake = new SelectList(modelRepository.GetAllMakers(), "Id", "Name", vehicleModel.MakeId)

            };

            return View(modelEdit);
        }


        [HttpPost]
        public IActionResult Edit(ModelEditViewModel vehicleModelEdit)
        {
            if (ModelState.IsValid)
            {
                var model = new VehicleModel()
                {
                    Name = vehicleModelEdit.Name,
                    Id = vehicleModelEdit.Id,
                    Abrv = vehicleModelEdit.Abrv,
                    MakeId = vehicleModelEdit.MakeId.Value
                };

                modelRepository.SaveVehicleModel(model);
                TempData["message"] = $"{vehicleModelEdit.Name} je spremljen";
                return RedirectToAction("Administration", new { page = 1, makeid = model.MakeId, searchString=model.Name});
            }

            vehicleModelEdit.ListMake = new SelectList(modelRepository.GetAllMakers(), "Id", "Name", vehicleModelEdit.MakeId);
            return View(vehicleModelEdit);
        }

        public IActionResult Create() =>
            View("Edit", new ModelEditViewModel()
            {
                MakeId = 0,
                ListMake = new SelectList(modelRepository.GetAllMakers().Distinct(), "Id", "Name", new VehicleMake())
            });                                              
    }
}
