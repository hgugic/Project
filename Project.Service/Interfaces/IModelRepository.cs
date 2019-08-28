using Project.Service.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Interfaces
{
    public interface IModelRepository
    {
        /// <summary>
        /// Modeli vozila
        /// </summary>
        IQueryable<VehicleModel> VehicleModels { get; }

        /// <summary>
        /// Spremanje i editiranje modela
        /// </summary>
        /// <param name="vehicleModel">Model vozila</param>
        void SaveVehicleModel(VehicleModel vehicleModel);

        /// <summary>
        /// Brisanje modela
        /// </summary>
        /// <param name="modelId">Identifikator modela</param>
        /// <returns>Model vozila</returns>
        VehicleModel DeleteVehicleModel(int modelId);

        /// <summary>
        /// Sortiranje modela
        /// </summary>
        /// <param name="sortBy">Sortiraj po vrsti</param>
        /// <param name="sortByDescending">Da li je padajući popis</param>
        /// <param name="vehicleModels">Proizvođači</param>
        /// <returns>IEnumerable</returns>
        IEnumerable<VehicleModel> SortBy(VehicleData sortBy, bool sortByDescending, IEnumerable<VehicleModel> vehicleModels);

        /// <summary>
        /// Priprema stranica
        /// </summary>
        /// <param name="page">Trenutna stranica</param>
        /// <param name="pageSize">Veličina stranica (broj modela na stranici)</param>
        /// <param name="makeId">Identifikator proizvođača</param>
        /// <param name="vehicleModels">Kolekcija modela</param>
        /// <returns></returns>
        IEnumerable<VehicleModel> Paging(int page, int pageSize, int? makeId, IEnumerable<VehicleModel> vehicleModels);

        /// <summary>
        /// Pretraživanje modela
        /// </summary>
        /// <param name="search">Uzorak za pretraživanje</param>
        /// <param name="vehicleModels">Modeli vozila</param>
        /// <param name="filter">Ograničenja u pretraživanju</param>
        /// <returns>Nađeni modeli</returns>
        IEnumerable<VehicleModel> Search(string search, IEnumerable<VehicleModel> vehicleModels, VehicleData filter);

        /// <summary>
        /// Dohvaćanje proizvođača
        /// </summary>
        /// <returns>Proizvođači vozila vozila</returns>
        IEnumerable<VehicleMake> GetAllMakers();
    }
}
