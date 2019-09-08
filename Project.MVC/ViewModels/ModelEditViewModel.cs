using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.Models;
using Project.Service.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za kreiranje ili editiranje VehicleModel klase - u propertije uključena su ograničenja
    /// </summary>
    public class ModelEditViewModel
    {

        public Model VehicleModel { get; set; }

        public SelectList ListMake { get; set; }

        private SelectListItem Item(string text, int value)
        {
            return new SelectListItem() { Text = text, Value = value.ToString() };
        }

        private List<SelectListItem> list = new List<SelectListItem>();

        public void SelectList(IVehicleService makeRepository)
        {
            if (VehicleModel == null)
                VehicleModel = new Model() { Id = 0, MakeId = 0 };

            foreach (var item in makeRepository.FindMake(out int i))
            {
                list.Add(Item(item.Name, item.Id));
            }

            ListMake = new SelectList(list.ToArray(), "Value", "Text", this.VehicleModel.MakeId);
        }
    }
}
