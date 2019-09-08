using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Models
{
    public class Model : IVehicleModel
    {
        public Model()
        {

        }

        public Model(IVehicleModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Abrv = model.Abrv;
            MakeId = model.MakeId;
        }
        public int Id { get; set; }

        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [MaxLength(30, ErrorMessage = "Predugačko ime")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Skraćenica je obavezno polje")]
        [Display(Name = "Skraćenica")]
        [MaxLength(10, ErrorMessage = "Skraćenica je predugačka")]
        public string Abrv { get; set; }

        [Required(ErrorMessage = "Proizvođač je obavezno polje")]
        [Display(Name = "Proizvođač")]
        public int MakeId { get; set; }

        public Make Make { get; set; }
    }
}
