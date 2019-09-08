using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interfaces
{
    public interface IVehicleService
    {
        #region Make

        IVehicleMake GetMakeById(int id);

        void SaveChanges(IVehicleMake vehicleMake);

        IVehicleMake DeleteMake(int id);

        IEnumerable<IVehicleMake> FindMake(out int totalPages,
                                           string searchString = "",
                                           string filter = "",
                                           string sortBy = "",
                                           int itemsPerPage = 0,
                                           int page = 0);

        #endregion Make

        #region Model

        IVehicleModel GetModelById(int id);

        void SaveChanges(IVehicleModel vehicleModel);

        IVehicleModel DeleteModel(int id);

        IEnumerable<IVehicleModel> FindModel(out int totalPages, 
                                             string searchString = "", 
                                             string filter = "", 
                                             string sortBy = "", 
                                             int itemsPerPage = 0, 
                                             int page = 0);

        #endregion Model
    }
}
