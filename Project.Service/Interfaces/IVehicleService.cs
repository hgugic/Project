using Project.Models.Interfaces;
using Project.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Interfaces
{
    public interface IVehicleService
    {
        #region Make

        IMake GetMakeById(int id);

        void SaveChanges(IMake vehicleMake);

        IMake DeleteMake(int id);

        IEnumerable<IMake> FindMake(IFilter filter, ISort sort, IPaging paging);

        #endregion Make

        #region Model

        IModel GetModelById(int id);

        void SaveChanges(IModel vehicleModel);

        IModel DeleteModel(int id);

        IEnumerable<IModel> FindModel(IFilter filter, ISort sort, IPaging paging);

        #endregion Model
    }
}
