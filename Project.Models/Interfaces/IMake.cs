using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Models.Interfaces
{
    public interface IMake
    {

        int Id { get; set; }
        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [MaxLength(30, ErrorMessage = "Predugačko ime")]
        string Name { get; set; }
        [Required(ErrorMessage = "Skraćenica je obavezno polje")]
        [Display(Name = "Skraćenica")]
        [MaxLength(10, ErrorMessage = "Skraćenica je predugačka")]
        string Abrv { get; set; }
    }
}
