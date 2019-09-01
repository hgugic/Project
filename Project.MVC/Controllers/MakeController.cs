using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {

        public int PageSize = 3;
        private readonly IMakeRepository makeRepository;

        public MakeController(IMakeRepository makeRepository)
        {
            this.makeRepository = makeRepository;
        }

        public ViewResult Administration(string searchString, string currentFilter, string sortBy, int page = 1)
        {
            MakeAdministrationViewModel viewModel = new MakeAdministrationViewModel();

            viewModel.SearchString = searchString;
            viewModel.SortBy = sortBy;

            viewModel.VehicleMakers = makeRepository.VehicleMakers()
                                                    .Find(searchString)
                                                    .SortBy(sortBy)
                                                    .Pagination(PageSize, page)
                                                    .ToCollection();


            viewModel.PagingInfo = makeRepository.PagingInfo();


            return View(viewModel);
        }


        public IActionResult Delete(int makeId)
        {
            makeRepository.Delete(makeId);
            return RedirectToAction("Administration", "Make", new { page = 1 });
        }
        public ViewResult Edit(int makeId)
        {
            return View(makeRepository.GetById(makeId));
        }


        [HttpPost]
        public IActionResult Edit(VehicleMake vehicleMakeEdit)
        {
            if (ModelState.IsValid)
            {

                makeRepository.SaveChanges(vehicleMakeEdit);
                return RedirectToAction("Administration", "Make", new { page = 1});
            }
            return View(vehicleMakeEdit);
        }

        public IActionResult Create() => View("Edit", new VehicleMake());
    }
}
