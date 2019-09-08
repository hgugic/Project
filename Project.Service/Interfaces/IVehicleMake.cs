using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Service.Interfaces
{
    public interface IVehicleMake
    {
        [Key]
        int Id { get; set; }
        [Required]
        string Name { get; set; }
        [MaxLength(10)]
        string Abrv { get; set; }
    }
}
