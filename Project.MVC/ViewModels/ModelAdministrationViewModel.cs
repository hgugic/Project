﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service;
using System.Collections.Generic;

namespace Project.MVC.ViewModels
{
    /// <summary>
    /// View model za sortiranje, pretraživanje, filtriranje i oznaku stranica VehicleModel klase 
    /// </summary>
    public class ModelAdministrationViewModel
    {
        public ModelAdministrationViewModel()
        {
            SortingList = new SelectList(GetSortingItems(), "Value", "Text", this.SortBy);
            FilteringList = new SelectList(GetFilteringItems(), "Value", "Text", this.SearchFilter);
        }

        public IEnumerable<VehicleModel> VehicleModels { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public int? CurrentMakeId { get; set; }

        public string SearchString { get; set; }

        public string SearchFilter { get; set; }

        public string SortBy { get; set; }

        public SelectList SortingList { get; set; }

        public SelectList FilteringList { get; set; }

        private IEnumerable<SelectListItem> GetSortingItems()
        {
            return new SelectListItem[]
            {
                new SelectListItem() { Text = "Izaberite", Value = string.Empty },
                new SelectListItem() { Text = "ID", Value = "Id" },
                new SelectListItem() { Text = "ID - padajući", Value = "Id_desc" },
                new SelectListItem() { Text = "Naziv", Value = "Name" },
                new SelectListItem() { Text = "Naziv - padajući", Value = "Name_desc" },
                new SelectListItem() { Text = "Skraćenica", Value = "Abrv" },
                new SelectListItem() { Text = "Skraćenica - padajući", Value = "Abrv_desc" },
                new SelectListItem() { Text = "Proizvođač", Value = "Make" },
                new SelectListItem() { Text = "Proizvođač - padajući", Value = "Make_desc" },
            };
        }

        private IEnumerable<SelectListItem> GetFilteringItems()
        {
            return new SelectListItem[]
            {
                new SelectListItem() { Text = "Izaberite", Value = string.Empty },
                new SelectListItem() { Text = "ID", Value = "Id" },
                new SelectListItem() { Text = "Naziv", Value = "Name" },
                new SelectListItem() { Text = "Skraćenica", Value = "Abrv" },
                new SelectListItem() { Text = "Proizvođač", Value = "Make" },
            };
        }
    }
}
