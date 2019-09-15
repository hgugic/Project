using Project.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Models
{
    public class Model : IModel
    {
        public int Id { get; set; }
        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [MaxLength(30, ErrorMessage = "Predugačko ime")]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Abrv { get; set; }

        public int MakeId { get; set; }

        public IMake Make { get; set; }
    }
}
