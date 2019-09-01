using System.Collections.Generic;

namespace Project.Service.Interfaces
{
    public interface IMakeRepository
    {
        VehicleMake GetById(int id);

        /// <summary>
        /// Spremanje i editiranje proizvođača
        /// </summary>
        /// <param name="vehicleMake">Proizvođač</param>
        void SaveChanges(VehicleMake vehicleMake);

        /// <summary>
        /// Brisanje proizvođača
        /// </summary>
        /// <param name="makeId">Identifikator proizvođača</param>
        /// <returns>VehicleMake</returns>
        VehicleMake Delete(int id);

        IMakeRepository VehicleMakers();

        IMakeRepository Find(string searchString, string filter = "");

        IMakeRepository SortBy(string sortBy);

        IMakeRepository Pagination(int itemsPerPage, int page = 1);

        PagingInfo PagingInfo();

        IEnumerable<VehicleMake> ToCollection();


    }
}
