using Microsoft.EntityFrameworkCore;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service
{
    public class EFVehicleService : IVehicleService  
    {
        private readonly VehicleDbContext context;
        IEnumerable<IVehicleModel> Models;
        IEnumerable<IVehicleMake> Makers;

        public EFVehicleService(VehicleDbContext context)
        {
            this.context = context;
        }

        #region Model

        public IVehicleModel GetModelById(int id)
        {
            return context.VehicleModels.FirstOrDefault(x => x.Id == id);
        }

        public IVehicleModel DeleteModel(int id)
        {
            VehicleModel model = context.VehicleModels.Find(id);
            if (model != null)
            {
                context.Remove(model);
                context.SaveChanges();
            }
            return model;
        }

        public void SaveChanges(IVehicleModel vehicleModel)
        {

            if (vehicleModel.Id == 0)
            {
                context.Add(ConvertToVehicleModel(vehicleModel));
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleModels.Attach(ConvertToVehicleModel(vehicleModel));
                vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<IVehicleModel> FindModel(out int totalPages, string searchString = "", string filter = "", string sortBy = "", int itemsPerPage = 0, int page = 0)
        {
            Models = context.VehicleModels;
            ModelListFind(searchString, filter);
            ModelListSortBy(sortBy);
            ModelListPaging(itemsPerPage, out totalPages, page);
            return Models;
        }

        private void ModelListFind(string searchString, string filter = "")
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();

                switch (filter)
                {
                    case "Name":
                        Models = Models.Where(x => x.Name.ToUpper().Contains(searchString));
                        break;

                    case "Id":
                        Models = Models.Where(x => x.Id.ToString() == searchString);
                        break;

                    case "Abrv":
                        Models = Models.Where(x => x.Abrv.ToUpper().Contains(searchString));
                        break;

                    case "Make":
                        IEnumerable<VehicleModel> ModelsWithMake = context.VehicleModels.Include(x => x.Make)
                                                                            .Where(x => x.Make.Name.ToUpper()
                                                                                                   .Contains(searchString));

                        Models = Models.Intersect(ModelsWithMake, new VehicleModelIdComparer());
                        break;

                    case "MakeId":
                        Models = Models.Where(x => x.MakeId.ToString() == searchString);
                        break;

                    default:
                        Models = Models.Where(x => x.Name.ToUpper().Contains(searchString) ||
                                                   x.Id.ToString().Contains(searchString) ||
                                                   x.Abrv.ToUpper().Contains(searchString));

                        break;
                }
            }
        }

        private void ModelListPaging(int itemsPerPage, out int totalPages, int page = 1)
        {
            totalPages = 0;

            if (itemsPerPage != 0)
            {
                totalPages = (int)Math.Ceiling((decimal)Models.Count() / itemsPerPage);
                Models = Models.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }
        }

        private void ModelListSortBy(string sortBy)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Name_desc":
                        Models = Models.OrderBy(x => x.Name).Reverse();
                        break;
                    case "Id":
                        Models = Models.OrderBy(x => x.Id);
                        break;
                    case "Id_desc":
                        Models = Models.OrderBy(x => x.Id).Reverse();
                        break;
                    case "Abrv":
                        Models = Models.OrderBy(x => x.Abrv);
                        break;
                    case "Abrv_desc":
                        Models = Models.OrderBy(x => x.Abrv).Reverse();
                        break;
                    case "Make":
                        IEnumerable<VehicleModel> ModelsWithMakeAsc = context.VehicleModels.Include(x => x.Make).OrderBy(x => x.Make.Name);                    
                        Models = ModelsWithMakeAsc.Intersect(Models, new VehicleModelIdComparer());
                        break;
                    case "Make_desc":
                        IEnumerable<VehicleModel> ModelsWithMakeDsc = context.VehicleModels.Include(x => x.Make).OrderByDescending(x => x.Make.Name);
                        Models = ModelsWithMakeDsc.Intersect(Models, new VehicleModelIdComparer());
                        break;
                    default:
                        Models = Models.OrderBy(x => x.Name);
                        break;
                }
            }
        }

        private VehicleModel ConvertToVehicleModel(IVehicleModel vehicleModel)
        {
            return new VehicleModel()
            {
                Id = vehicleModel.Id,
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                MakeId = vehicleModel.MakeId
            };
        }

        #endregion Model

        #region Make

        public IVehicleMake GetMakeById(int id)
        {
            return context.VehicleMakers.FirstOrDefault(x => x.Id == id);
        }

        public IVehicleMake DeleteMake(int id)
        {
            VehicleMake make = context.VehicleMakers.Find(id);
            if (make != null)
            {
                context.Remove(make);
                context.SaveChanges();
            }
            return make;
        }

        public void SaveChanges(IVehicleMake vehicleMake)
        {
            if (vehicleMake.Id == 0)
            {
                context.Add(ConvertToVehicleMake(vehicleMake));
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleMakers.Attach(ConvertToVehicleMake(vehicleMake));
                vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<IVehicleMake> FindMake(out int totalPages, 
                                              string searchString = "", 
                                              string filter = "", 
                                              string sortBy = "", 
                                              int itemsPerPage = 0, 
                                              int page = 0)
        {
            Makers = context.VehicleMakers;
            MakeListFind(searchString, filter);
            MakeListSortBy(sortBy);
            MakeListPaging(itemsPerPage, out totalPages, page);
            return Makers;
        }

        private void MakeListFind(string searchString, string filter = "")
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();

                switch (filter)
                {
                    case "Name":
                        Makers = Makers.Where(x => x.Name.ToUpper().Contains(searchString));
                        break;

                    case "Id":
                        Makers = Makers.Where(x => x.Id.ToString() == searchString);
                        break;

                    case "Abrv":
                        Makers = Makers.Where(x => x.Abrv.ToUpper().Contains(searchString));
                        break;

                    default:
                        Makers = Makers.Where(x => x.Name.ToUpper().Contains(searchString) ||
                                                   x.Id.ToString().Contains(searchString) ||
                                                   x.Abrv.ToUpper().Contains(searchString));
                        break;
                }
            }

        }

        private void MakeListPaging(int itemsPerPage, out int totalPages, int page = 1)
        {
            totalPages = 0;

            if (itemsPerPage != 0)
            {
                totalPages = (int)Math.Ceiling((decimal)Makers.Count() / itemsPerPage);
                Makers = Makers.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }
        }

        private void MakeListSortBy(string sortBy)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Name_desc":
                        Makers = Makers.OrderBy(x => x.Name).Reverse();
                        break;
                    case "Id":
                        Makers = Makers.OrderBy(x => x.Id);
                        break;
                    case "Id_desc":
                        Makers = Makers.OrderBy(x => x.Id).Reverse();
                        break;
                    case "Abrv":
                        Makers = Makers.OrderBy(x => x.Abrv);
                        break;
                    case "Abrv_desc":
                        Makers = Makers.OrderBy(x => x.Abrv).Reverse();
                        break;
                    default:
                        Makers = Makers.OrderBy(x => x.Name);
                        break;
                }
            }
        }

        private VehicleMake ConvertToVehicleMake(IVehicleMake vehicleMake)
        {
            return new VehicleMake()
            {
                Id = vehicleMake.Id,
                Name = vehicleMake.Name,
                Abrv = vehicleMake.Abrv
            };
        }

        #endregion Make
    }
}
