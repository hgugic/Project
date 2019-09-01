using System.Collections.Generic;

namespace Project.Service.Interfaces
{
    public interface IModelRepository
    {
        VehicleModel GetById(int id);

        void SaveChanges(VehicleModel vehicleModel);

        VehicleModel Delete(int id);

        IModelRepository VehicleModels();

        IModelRepository Find(string searchString, string filter = "");

        IModelRepository SortBy(string sortBy);

        IModelRepository Pagination(int itemsPerPage, int page = 1);

        PagingInfo PagingInfo();

        IEnumerable<VehicleModel> ToCollection();

        IEnumerable<VehicleMake> VehicleMakers();
    }
}
