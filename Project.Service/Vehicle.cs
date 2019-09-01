using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Service
{
    /// <summary>
    /// Osnovna klasa modela
    /// </summary>
    public abstract class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Abrv { get; set; }
    }
}
