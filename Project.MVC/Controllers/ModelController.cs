using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public ViewResult Administration(string searchString, string searchFilter, string sortBy,  int page = 1)
        {
            ModelAdministrationViewModel viewModel = new ModelAdministrationViewModel();
          
            viewModel.SearchString = searchString;
            viewModel.SearchFilter = searchFilter;
            viewModel.SortBy = sortBy;

            viewModel.VehicleModels = modelRepository.VehicleModels()
                                                     .Find(searchString, searchFilter)
                                                     .SortBy(sortBy)
                                                     .Pagination(PageSize, page)
                                                     .ToCollection();

            viewModel.PagingInfo = modelRepository.PagingInfo();
            
            return View(viewModel);
        }


        public IActionResult Delete(int modelId)
        {
            modelRepository.Delete(modelId);
            return RedirectToAction("Administration", new { page = 1 });
        }


        public ViewResult Edit(int modelId)
        {
            var vehicleModel = modelRepository.GetById(modelId);
            var modelEdit = new ModelEditViewModel
            {
                Id = vehicleModel.Id,
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                ListMake = new SelectList(modelRepository.VehicleMakers(), "Id", "Name", vehicleModel.MakeId)

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

                modelRepository.SaveChanges(model);
                TempData["message"] = $"{vehicleModelEdit.Name} je spremljen";
                return RedirectToAction("Administration", new { page = 1, searchFilter = "Id", searchString=model.Id.ToString()});
            }

            vehicleModelEdit.ListMake = new SelectList(modelRepository.VehicleMakers(), "Id", "Name", vehicleModelEdit.MakeId);
            return View(vehicleModelEdit);
        }

        public IActionResult Create() =>
            View("Edit", new ModelEditViewModel()
            {
                MakeId = 0,
                ListMake = new SelectList(modelRepository.VehicleMakers(), "Id", "Name", new VehicleMake())
            });                                              
    }
}
