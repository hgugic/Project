using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service;
using System.ComponentModel.DataAnnotations;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za kreiranje ili editiranje VehicleModel klase - u propertije uključena su ograničenja
    /// </summary>
    public class ModelEditViewModel : CreateOrEditViewModel
    {
        [Display(Name = "Proizvođač")]
        [Required(ErrorMessage = "Proizvođač je obavezno polje")]
        public int? MakeId { get; set; }

        public VehicleMake Make { get; set; }

        
        public SelectList ListMake { get; set; }

    }
}
