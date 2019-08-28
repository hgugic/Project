using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Service
{
    /// <summary>
    /// Osnovna klasa modela
    /// </summary>
    public class Vehicle : IComparable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Abrv { get; set; }
        public int Compare(Vehicle x, Vehicle y)
        {
            if (x.Name.CompareTo(y.Name) != 0)
            {
                return x.Name.CompareTo(y.Name);
            }
            else
            {
                return 0;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Vehicle vehicle = obj as Vehicle;
            if (vehicle != null)
                return this.Name.CompareTo(vehicle.Name);
            else
                throw new ArgumentException("Objekt nije tipa Vehicle");
        }
    }
}
