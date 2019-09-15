using Project.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Models
{
    public class Make : IMake
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Abrv { get; set; }
    }
}
