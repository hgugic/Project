using Project.Service.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Interfaces
{
    public interface IMakeRepository
    {
        /// <summary>
        /// Popis proizvođača
        /// </summary>
        IQueryable<VehicleMake> VehicleMakers { get; }

        /// <summary>
        /// Spremanje i editiranje proizvođača
        /// </summary>
        /// <param name="vehicleMake">Proizvođač</param>
        void SaveVehicleMake(VehicleMake vehicleMake);

        /// <summary>
        /// Brisanje proizvođača
        /// </summary>
        /// <param name="makeId">Identifikator proizvođača</param>
        /// <returns>VehicleMake</returns>
        VehicleMake DeleteVehicleMake(int makeId);

        /// <summary>
        /// Sortiranje proizvođača
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="sortByDescending"></param>
        /// <param name="vehicleMakers"></param>
        /// <returns></returns>
        IEnumerable<VehicleMake> SortBy(VehicleData sortBy, bool sortByDescending, IEnumerable<VehicleMake> vehicleMakers);

        /// <summary>
        /// Priprema stranica
        /// </summary>
        /// <param name="page">Stranica za prikaz</param>
        /// <param name="pageSize">Veličina stranica (broj proizvođača na stranici)</param>
        /// <param name="vehicleMakers">Kolekcija</param>
        /// <returns>Proizvođači</returns>
        IEnumerable<VehicleMake> Paging(int page, int pageSize, IEnumerable<VehicleMake> vehicleMakers);

        /// <summary>
        /// Pretraživanje proizvođača
        /// </summary>
        /// <param name="search">Uzorak za pretraživanje</param>
        /// <param name="vehicleMakers">Kolekcija proizvođača</param>
        /// <returns>Pronađeni proizvođači</returns>
        IEnumerable<VehicleMake> Search(string search, IEnumerable<VehicleMake> vehicleMakers);


    }
}
