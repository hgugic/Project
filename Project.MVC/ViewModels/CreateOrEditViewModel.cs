using System.ComponentModel.DataAnnotations;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za kreiranje ili editiranje Vehicle klase - u propertije uključena su ograničenja
    /// </summary>
    public class CreateOrEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [MaxLength(30, ErrorMessage = "Predugačko ime")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Skraćenica je obavezno polje")]
        [Display(Name = "Skraćenica")]
        [MaxLength(10, ErrorMessage = "Skraćenica je predugačka")]
        public string Abrv { get; set; }

    }
}
