using System.ComponentModel.DataAnnotations;

namespace Project.Service.Enums
{
    public enum VehicleData
    {
        Unknown,
        Id,
        [Display(Name = "Naziv")]
        Name,
        [Display(Name = "Skraćenica")]
        Abrv,
        [Display(Name = "Proizvođač")]
        Make
    }
}
